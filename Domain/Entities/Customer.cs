namespace Domain.Entities;

public class Customer : EntityBase
{
    public decimal Balance { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public virtual IEnumerable<Purchase> Purchases { get; set; }
}