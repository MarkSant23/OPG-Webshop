namespace OpgWebshop.Domain.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int OrderNumber { get; set; }
    public Guid BuyerUserId { get; set; }
    public User BuyerUser { get; set; } = null!;
    public Guid DeliveryScheduleId { get; set; }
    public DeliverySchedule DeliverySchedule { get; set; } = null!;
    public Guid PickupHubId { get; set; }
    public PickupHub PickupHub { get; set; } = null!;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CashOnDelivery;
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public LeadForm? LeadForm { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
