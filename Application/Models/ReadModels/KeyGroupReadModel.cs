namespace Application.Models.ReadModels;

public class KeyGroupReadModel
{
    public PlatformReadModel Platform { get; set; } = new();
    public int GameId { get; set; }
    public string GameName { get; set; } = default!;
    public IEnumerable<CategoryReadModel> Categories { get; set; } = Enumerable.Empty<CategoryReadModel>();
    public string? PreviewImage { get; set; }
    public decimal MinPrice { get; set; }
}
