namespace OpgWebshop.Domain.Models;

public class LeadForm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid BuyerUserId { get; set; }
    public User BuyerUser { get; set; } = null!;

    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string? Message { get; set; }
    public LeadFormStatus Status { get; set; } = LeadFormStatus.Init;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
