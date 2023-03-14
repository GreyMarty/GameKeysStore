namespace Infrastructure.Security;

public class Token
{
    public string TokenString { get; set; }
    public DateTime ExpiresAt { get; set; }
}

public record TokenPair(Token AccessToken, Token RefreshToken);