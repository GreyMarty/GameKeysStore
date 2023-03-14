namespace Application.DTOs;

public class UserDto
{
    public int Id { get; private set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public DateTime RegisteredOn { get; private set; }

    public long RoleFlags { get; set; }
}