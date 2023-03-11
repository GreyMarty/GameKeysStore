namespace Domain.Entities;

public class Role : EntityBase
{
    public string Name { get; set; }

    public virtual IEnumerable<RoleMembership> Members { get; set; }
}