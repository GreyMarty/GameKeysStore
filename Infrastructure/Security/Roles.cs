namespace Infrastructure.Security;

[Flags]
public enum RoleFlags
{
    Admin = 1,
    Manager = 2,
    User = 4
}

public class Roles
{
    public static readonly Roles Admin = new(RoleFlags.Admin.ToString().ToLower());
    public static readonly Roles Manager = new(RoleFlags.Manager.ToString().ToLower());
    public static readonly Roles User = new(RoleFlags.User.ToString().ToLower());

    private readonly string _rolesString;

    public Roles(string rolesString)
    {
        _rolesString = rolesString;
    }

    public static implicit operator string(Roles roles)
    {
        return roles._rolesString;
    }

    public static Roles operator |(Roles a, Roles b)
    {
        return new Roles($"{a},{b}");
    }
}