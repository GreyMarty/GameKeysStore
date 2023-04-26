namespace Domain.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }

    public virtual IEnumerable<Game> Games { get; set; }
}
