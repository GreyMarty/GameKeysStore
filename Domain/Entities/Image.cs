namespace Domain.Entities;

public class Image : EntityBase
{
    public int FileId { get; set; }
    public PhysicalFile File { get; set; }
}
