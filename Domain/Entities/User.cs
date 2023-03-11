namespace Domain.Entities;

public class User : EntityBase
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime RegisteredOn { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual IEnumerable<RoleMembership> Memberships { get; set; }
}