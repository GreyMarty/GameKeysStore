using Infrastructure.Security.Exceptions;
using Infrastructure.Security.Helpers;
using Infrastructure.Security.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using UI.Authentication.Models;

namespace UI.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "CustomAuth";
    private const string UserSessionKey = "UserSession";

    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    private readonly ProtectedLocalStorage _storage;
    private readonly IIdentityService _identityService;
    private readonly IPasswordHelper _passwordHelper;

    public CustomAuthenticationStateProvider(IIdentityService identityService, IPasswordHelper passwordHelper, ProtectedLocalStorage storage)
    {
        _identityService = identityService;
        _passwordHelper = passwordHelper;
        _storage = storage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try 
        {
            var userSessionResult = await _storage.GetAsync<UserSession>(UserSessionKey);

            var userSession = userSessionResult.Success
                ? userSessionResult.Value
                : null;

            if (userSession is null)
            {
                return new AuthenticationState(_anonymous);
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userSession.UserName),
                new Claim(ClaimTypes.Role, userSession.Role ?? String.Empty)
            }, AuthenticationType));

            return new AuthenticationState(claimsPrincipal);
        }
        catch 
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task LoginAsync(string login, string password) 
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

        var userSession = new UserSession
        {
            UserName = user.UserName,
            Role = _identityService.GetUserRole(user)
        };

        await _storage.SetAsync(UserSessionKey, userSession);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] 
        {
            new Claim(ClaimTypes.Name, userSession.UserName),
            new Claim(ClaimTypes.Role, userSession.Role ?? String.Empty)
        }, AuthenticationType));

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task LogOutAsync() 
    {
        await _storage.DeleteAsync(UserSessionKey);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}
