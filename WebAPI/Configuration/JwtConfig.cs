using System.Text;
using Infrastructure.Security.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Configuration;

public class JwtConfig : IJwtConfig
{
    private readonly IConfiguration _config;

    public string Issuer => _config["JWT:Issuer"];

    public string Audience => _config["JWT:Audience"];

    public TimeSpan AccessTokenLifetime =>
        TimeSpan.FromMinutes(Convert.ToDouble(_config["JWT:AccessTokenLifetimeMinutes"]));

    public TimeSpan RefreshTokenLifetime =>
        TimeSpan.FromHours(Convert.ToDouble(_config["JWT:RefreshTokenLifetimeHours"]));

    public SymmetricSecurityKey Key => new(Encoding.ASCII.GetBytes(_config["JWT:Key"]));


    public JwtConfig(IConfiguration config)
    {
        _config = config;
    }
}