namespace OpgWebshop.Domain.Models;

public class OpgProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string DisplayName { get; set; } = string.Empty;
    public string Oib { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public OpgApprovalStatus Status { get; set; } = OpgApprovalStatus.Pending;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedAtUtc { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public User? ApprovedByUser { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
