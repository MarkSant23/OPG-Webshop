namespace OpgWebshop.Domain.Models;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int? PublicId { get; set; }
    public Guid OpgProfileId { get; set; }
    public OpgProfile OpgProfile { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public ProductInventory? Inventory { get; set; }
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
