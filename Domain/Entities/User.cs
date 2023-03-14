namespace Domain.Entities;

public class User : EntityBase
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Salt { get; set; }
    public string Password { get; set; }

    public DateTime RegisteredOn { get; set; }

    public ulong RoleFlags { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }

    public virtual Customer? Customer { get; set; }
}