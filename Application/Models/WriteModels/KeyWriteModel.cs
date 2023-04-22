namespace Application.Models.WriteModels;

public class KeyWriteModel
{
    public int GameId { get; set; }
    public string KeyString { get; set; } = default!;
    public PlatformWriteModel Platform { get; set; } = default!;
    public decimal Price { get; set; }
}
