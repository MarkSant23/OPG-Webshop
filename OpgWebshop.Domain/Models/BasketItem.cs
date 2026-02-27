namespace OpgWebshop.Domain.Models;

public class BasketItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPriceAtAdd { get; set; }
}
