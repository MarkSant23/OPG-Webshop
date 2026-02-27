namespace OpgWebshop.Domain.Models;

public class ProductInventory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int QuantityAvailable { get; set; }
    public int QuantityReserved { get; set; }
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
