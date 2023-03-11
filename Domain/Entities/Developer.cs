namespace Domain.Entities;

public class Developer : EntityBase
{
    public string Name { get; set; }

    public virtual IEnumerable<Game> Games { get; set; }
}