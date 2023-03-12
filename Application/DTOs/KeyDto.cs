namespace Application.DTOs;

public class KeyDto
{
    public int Id { get; private set; }
    public int GameId { get; private set; }
    public string KeyString { get; set; }
    public PlatformDto Platform { get; set; }
    public decimal Price { get; set; }
    public bool Purchased { get; private set; }
}