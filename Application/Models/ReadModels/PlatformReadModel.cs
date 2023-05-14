namespace Application.Models.ReadModels;

public class PlatformReadModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int GamesCount { get; set; }
}
