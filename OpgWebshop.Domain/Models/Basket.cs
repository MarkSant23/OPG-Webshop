namespace OpgWebshop.Domain.Models;

public class Basket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BuyerUserId { get; set; }
    public User BuyerUser { get; set; } = null!;
    public DateTime LastUpdatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
}
