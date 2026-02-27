namespace OpgWebshop.Domain.Models;

public class PickupHub
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<DeliverySchedule> DeliverySchedules { get; set; } = new List<DeliverySchedule>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
