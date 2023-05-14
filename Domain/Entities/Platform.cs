namespace Domain.Entities;

public class Platform : EntityBase
{
    public string Name { get; set; }

    public virtual IEnumerable<Key> Keys { get; set; }
}