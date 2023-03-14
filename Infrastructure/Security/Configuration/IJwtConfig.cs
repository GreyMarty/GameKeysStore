using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security.Configuration;

public interface IJwtConfig
{
    public string Issuer { get; }
    public string Audience { get; }
    public TimeSpan AccessTokenLifetime { get; }
    public TimeSpan RefreshTokenLifetime { get; }
    public SymmetricSecurityKey Key { get; }
}