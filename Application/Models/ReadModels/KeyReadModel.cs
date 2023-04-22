namespace Application.Models.ReadModels;

public class KeyReadModel
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public string KeyString { get; set; } = default!;
    public PlatformReadModel Platform { get; set; } = default!;
    public decimal Price { get; set; }
    public bool Purchased { get; set; }
}
