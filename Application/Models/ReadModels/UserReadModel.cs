namespace Application.Models.ReadModels;

public class UserReadModel
{
    public int Id { get; private set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime RegisteredOn { get; set; }
    public long RoleFlags { get; set; } = default!;
}
