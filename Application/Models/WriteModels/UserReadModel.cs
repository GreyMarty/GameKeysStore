namespace Application.Models.WriteModels;

public class UserWriteModel
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Password { get; set; }
    public long RoleFlags { get; set; } = default!;
}
