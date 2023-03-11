namespace Domain.Entities;

public class SystemRequirements : EntityBase
{
    public string OperatingSystem { get; set; }

    public string Processor { get; set; }

    public string Memory { get; set; }

    public string Graphics { get; set; }

    public string Storage { get; set; }

    public virtual Game Game { get; set; }
}