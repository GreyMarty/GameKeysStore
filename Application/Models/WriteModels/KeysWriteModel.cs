namespace Application.Models.WriteModels;

public class KeysWriteModel
{
    public string GameName { get; set; }
    public IEnumerable<string> KeyStrings { get; set; } = Enumerable.Empty<string>();
    public PlatformWriteModel Platform { get; set; } = new();
    public decimal Price { get; set; }
}
