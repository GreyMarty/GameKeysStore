using System.Security.Claims;
using Application.Results;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Security.Helpers;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Security.Services;

public interface ITokenService
{
    public OneOf<TokenPair, ValidationFailed, NotFound> CreateToken(string login, string password);
}

[Service(ServiceLifetime.Scoped)]
public class TokenService : ITokenService
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IIdentityService _identityService;
    private readonly IUsersRepo _usersRepo;

    public TokenService(
        ITokenHelper tokenHelper,
        IPasswordHelper passwordHelper,
        IUsersRepo usersRepo,
        IIdentityService identityService)
    {
        _tokenHelper = tokenHelper;
        _passwordHelper = passwordHelper;
        _usersRepo = usersRepo;
        _identityService = identityService;
    }

    public OneOf<TokenPair, ValidationFailed, NotFound> CreateToken(string login, string password)
    {
        var userFound = _identityService.FindUserByLogin(login);

        if (userFound.IsT1) return new NotFound();

        var user = userFound.AsT0;

        if (!_passwordHelper.VerifyPassword(password, user.Password, user.Salt))
            return new ValidationFailed(nameof(password), "Wrong password");

        var refreshToken = _tokenHelper.GenerateRefreshToken();
        user.RefreshToken = refreshToken.TokenString;
        user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

        _usersRepo.Update(user);

        var claims = _identityService.GetRolesFromFlags((RoleFlags)user.RoleFlags)
            .Select(r => new Claim(ClaimTypes.Role, r))
            .Append(new Claim(ClaimTypes.Name, user.UserName))
            .Append(new Claim(ClaimTypes.Email, user.Email));

        var accessToken = _tokenHelper.GenerateAccessToken(claims);
        return new TokenPair(accessToken, refreshToken);
    }
}