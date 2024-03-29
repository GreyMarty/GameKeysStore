﻿namespace Application.Models.ReadModels;

public class SystemRequirementsReadModel
{
    public string OperatingSystem { get; set; } = default!;
    public string Processor { get; set; } = default!;
    public string Memory { get; set; } = default!;
    public string Graphics { get; set; } = default!;
    public string Storage { get; set; } = default!;
}
