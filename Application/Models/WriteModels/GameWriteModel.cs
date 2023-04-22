namespace Application.Models.WriteModels;

public class GameWriteModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DeveloperWriteModel Developer { get; set; } = default!;
    public SytemRequirementsWriteModel RecommendedSystemRequirements { get; set; } = default!;
}
