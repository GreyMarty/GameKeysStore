namespace Application.Models.WriteModels;

public class GameWriteModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DeveloperWriteModel Developer { get; set; } = new();
    public SystemRequirementsWriteModel RecommendedSystemRequirements { get; set; } = new();
    public IEnumerable<int> CategoryIds { get; set; } = Enumerable.Empty<int>();
    public ICollection<string> Images { get; set; } = new List<string>();
}
