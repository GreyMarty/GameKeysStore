namespace Application.Models.ReadModels;

public class GameReadModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Rating { get; set; }
    public DeveloperReadModel Developer { get; set; } = default!;
    public SytemRequirementsReadModel RecommendedSystemRequirements { get; set; } = default!;
}
