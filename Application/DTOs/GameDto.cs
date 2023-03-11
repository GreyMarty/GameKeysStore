namespace Application.Models;

public class GameDto
{
    public int Id { get; private set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Rating { get; private set; }

    public DeveloperDto Developer { get; set; }

    public SystemRequirementsDto RecommendedSystemRequirements { get; set; }
}