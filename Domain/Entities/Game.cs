namespace Domain.Entities;

public class Game : EntityBase
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int Rating { get; set; }

    public int DeveloperId { get; set; }
    public Developer Developer { get; set; }

    public int RecommendedSystemRequirementsId { get; set; }
    public SystemRequirements RecommendedSystemRequirements { get; set; }

    public bool Deleted { get; set; } = false;

    public virtual ICollection<Key> Keys { get; set; }

    public virtual ICollection<Category> Categories { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}