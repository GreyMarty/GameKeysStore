using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Services;
using Infrastructure.Security.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security.Helpers;

public interface ITokenHelper
{
    Token GenerateAccessToken(IEnumerable<Claim> claims);
    Token GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

[Service(ServiceLifetime.Scoped)]
public class TokenHelper : ITokenHelper
{
    private readonly IJwtConfig _jwtConfig;

    public TokenHelper(IJwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }

    public Token GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.UtcNow.Add(_jwtConfig.AccessTokenLifetime),
            signingCredentials: new SigningCredentials(_jwtConfig.Key, SecurityAlgorithms.HmacSha512Signature)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new Token
        {
            TokenString = tokenString,
            ExpiresAt = tokenOptions.ValidTo
        };
    }

    public Token GenerateRefreshToken()
    {
        var randomBytes = new byte[32];

        using (var random = RandomNumberGenerator.Create())
        {
            random.GetBytes(randomBytes);
        }

        return new Token
        {
            TokenString = Convert.ToBase64String(randomBytes),
            ExpiresAt = DateTime.UtcNow.Add(_jwtConfig.RefreshTokenLifetime)
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtConfig.Issuer,

            ValidateAudience = true,
            ValidAudience = _jwtConfig.Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _jwtConfig.Key
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal;
        SecurityToken validatedToken;

        try
        {
            principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
        }
        catch (ArgumentException)
        {
            throw new SecurityTokenException("Invalid token");
        }

        var jwtToken = validatedToken as JwtSecurityToken;

        if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature,
                StringComparison.InvariantCultureIgnoreCase)) throw new SecurityTokenException("Invalid token");

        return principal;
    }
}