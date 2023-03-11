namespace Domain.Entities;

public class Key : EntityBase
{
    public string KeyString { get; set; }
    public decimal Price { get; set; }
    public bool Purchased { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public int PlatformId { get; set; }
    public Platform Platform { get; set; }

    public virtual Purchase? Purchase { get; set; }
}