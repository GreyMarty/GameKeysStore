namespace Domain.Entities;

public class Purchase : EntityBase
{
    public DateTime PurchasedOn { get; set; }

    public int KeyId { get; set; }
    public Key Key { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}