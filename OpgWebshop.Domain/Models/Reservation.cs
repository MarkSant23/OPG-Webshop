namespace OpgWebshop.Domain.Models;

public class Reservation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid BuyerUserId { get; set; }
    public User BuyerUser { get; set; } = null!;
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
    public int Quantity { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Active;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
