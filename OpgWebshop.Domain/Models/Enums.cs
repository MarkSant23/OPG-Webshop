namespace OpgWebshop.Domain.Models;

public enum UserStatus
{
    PendingApproval = 0,
    Active = 1,
    Inactive = 2
}

public enum OpgApprovalStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Delivered = 2,
    Cancelled = 3
}

public enum PaymentMethod
{
    CashOnDelivery = 0
}

public enum ReservationStatus
{
    Active = 0,
    ConvertedToOrder = 1,
    Expired = 2,
    Cancelled = 3
}

public enum EmailStatus
{
    Queued = 0,
    Sent = 1,
    Failed = 2
}

public enum LeadFormStatus
{
    Init = 0,
    Done = 1,
    Canceled = 2
}
