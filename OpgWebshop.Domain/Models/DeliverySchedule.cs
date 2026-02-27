namespace OpgWebshop.Domain.Models;

public class DeliverySchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PickupHubId { get; set; }
    public PickupHub PickupHub { get; set; } = null!;
    public DateTime DeliveryDateUtc { get; set; }
    public TimeSpan TimeFrom { get; set; }
    public TimeSpan TimeTo { get; set; }
    public string WindowLabel { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int ReservedCount { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
