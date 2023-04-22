using System.Security.Claims;
using Application;
using Application.Services;
using Infrastructure.Security.Exceptions;
using Infrastructure.Security.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Security.Services;

[Service(ServiceLifetime.Scoped)]
public class TokenService
{
    private readonly IApplicationDbContext _db;
    private readonly ITokenHelper _tokenHelper;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IIdentityService _identityService;

    public TokenService(
        ITokenHelper tokenHelper,
        IPasswordHelper passwordHelper,
        IIdentityService identityService,
        IApplicationDbContext db)
    {
        _tokenHelper = tokenHelper;
        _passwordHelper = passwordHelper;
        _identityService = identityService;
        _db = db;
    }

    public async Task<TokenPair> CreateTokenAsync(string login, string password)
    {
        var user = await _identityService.FindUserByLoginAsync(login);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (!_passwordHelper.VerifyPassword(password, user.Password, user.Salt))
        {
            throw new WrongPasswordException();
        }

        var refreshToken = _tokenHelper.GenerateRefreshToken();
        user.RefreshToken = refreshToken.TokenString;
        user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

        _db.Users.Update(user);

        var claims = _identityService.GetRolesFromFlags((RoleFlags)user.RoleFlags)
            .Select(r => new Claim(ClaimTypes.Role, r))
            .Append(new Claim(ClaimTypes.Name, user.UserName))
            .Append(new Claim(ClaimTypes.Email, user.Email));

        var accessToken = _tokenHelper.GenerateAccessToken(claims);
        return new TokenPair(accessToken, refreshToken);
    }
}