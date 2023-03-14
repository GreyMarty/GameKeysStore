using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Results;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Security.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Security.Services;

public interface ITokenService
{
    public Token GenerateAccessToken(IEnumerable<Claim> claims);
    public Token GenerateRefreshToken();
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    public OneOf<TokenPair, ValidationFailed, NotFound> CreateToken(string login, string password);
}

[Service(ServiceLifetime.Scoped)]
public class TokenService : ITokenService
{
    private readonly IJwtConfig _jwtConfig;
    private readonly IIdentityService _identityService;
    private readonly IUsersRepo _usersRepo;

    public TokenService(
        IJwtConfig jwtConfig,
        IUsersRepo usersRepo,
        IIdentityService identityService)
    {
        _jwtConfig = jwtConfig;
        _usersRepo = usersRepo;
        _identityService = identityService;
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

    public OneOf<TokenPair, ValidationFailed, NotFound> CreateToken(string login, string password)
    {
        var userFound = _identityService.FindUserByLogin(login);

        if (userFound.IsT1) return new NotFound();

        var user = userFound.AsT0;

        if (!PasswordHelper.VerifyPassword(password, user.Password, user.Salt))
            return new ValidationFailed(nameof(password), "Wrong password");

        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken.TokenString;
        user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

        _usersRepo.Update(user);

        var claims = _identityService.GetRolesFromFlags((RoleFlags)user.RoleFlags)
            .Select(r => new Claim(ClaimTypes.Role, r))
            .Append(new Claim(ClaimTypes.Name, user.UserName))
            .Append(new Claim(ClaimTypes.Email, user.Email));

        var accessToken = GenerateAccessToken(claims);
        return new TokenPair(accessToken, refreshToken);
    }
}