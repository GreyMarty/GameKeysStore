﻿namespace Application.Models.WriteModels;

public class KeyWriteModel
{
    public string GameName { get; set; }
    public string KeyString { get; set; } = default!;
    public PlatformWriteModel Platform { get; set; } = new();
    public decimal Price { get; set; }
}
