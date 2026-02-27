using Microsoft.EntityFrameworkCore;
using OpgWebshop.Domain.Models;

namespace OpgWebshop.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<OpgProfile> OpgProfiles => Set<OpgProfile>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductInventory> ProductInventories => Set<ProductInventory>();
    public DbSet<Basket> Baskets => Set<Basket>();
    public DbSet<BasketItem> BasketItems => Set<BasketItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<LeadForm> LeadForms => Set<LeadForm>();
    public DbSet<DeliverySchedule> DeliverySchedules => Set<DeliverySchedule>();
    public DbSet<PickupHub> PickupHubs => Set<PickupHub>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<EmailQueueItem> EmailQueueItems => Set<EmailQueueItem>();
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();
    public DbSet<CmsPage> CmsPages => Set<CmsPage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasSequence<int>("OrderNumbers")
            .StartsAt(10001)
            .IncrementsBy(1);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(64).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.Email).HasMaxLength(256).IsRequired();
            entity.Property(x => x.PasswordHash).HasMaxLength(512).IsRequired();
            entity.Property(x => x.FirstName).HasMaxLength(128).IsRequired();
            entity.Property(x => x.LastName).HasMaxLength(128).IsRequired();
            entity.Property(x => x.PhoneNumber).HasMaxLength(32).IsRequired();
            entity.HasOne(x => x.Role).WithMany(r => r.Users).HasForeignKey(x => x.RoleId);
        });

        modelBuilder.Entity<OpgProfile>(entity =>
        {
            entity.HasIndex(x => x.UserId).IsUnique();
            entity.HasIndex(x => x.Oib).IsUnique();
            entity.Property(x => x.DisplayName).HasMaxLength(256).IsRequired();
            entity.Property(x => x.Oib).HasMaxLength(32).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(256).IsRequired();
            entity.Property(x => x.City).HasMaxLength(128).IsRequired();
            entity.HasOne(x => x.User).WithOne(u => u.OpgProfile).HasForeignKey<OpgProfile>(x => x.UserId);
            entity.HasOne(x => x.ApprovedByUser).WithMany().HasForeignKey(x => x.ApprovedByUserId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(128).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(1024);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(x => x.CategoryId);
            entity.HasIndex(x => x.OpgProfileId);
            entity.HasIndex(x => x.PublicId).IsUnique().HasFilter("[PublicId] IS NOT NULL");
            entity.Property(x => x.Name).HasMaxLength(256).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.Property(x => x.Price).HasPrecision(18, 2);
            entity.HasOne(x => x.OpgProfile).WithMany(o => o.Products).HasForeignKey(x => x.OpgProfileId);
            entity.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.CategoryId);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.Property(x => x.Url).HasMaxLength(1024).IsRequired();
            entity.HasOne(x => x.Product).WithMany(p => p.Images).HasForeignKey(x => x.ProductId);
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.HasIndex(x => x.ProductId).IsUnique();
            entity.Property(x => x.RowVersion).IsRowVersion();
            entity.HasOne(x => x.Product).WithOne(p => p.Inventory).HasForeignKey<ProductInventory>(x => x.ProductId);
        });

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasIndex(x => x.BuyerUserId).IsUnique();
            entity.HasOne(x => x.BuyerUser).WithOne(u => u.Basket).HasForeignKey<Basket>(x => x.BuyerUserId);
        });

        modelBuilder.Entity<BasketItem>(entity =>
        {
            entity.HasIndex(x => new { x.BasketId, x.ProductId }).IsUnique();
            entity.Property(x => x.UnitPriceAtAdd).HasPrecision(18, 2);
            entity.HasOne(x => x.Basket).WithMany(b => b.Items).HasForeignKey(x => x.BasketId);
            entity.HasOne(x => x.Product).WithMany(p => p.BasketItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<DeliverySchedule>(entity =>
        {
            entity.HasIndex(x => new { x.DeliveryDateUtc, x.PickupHubId });
            entity.Property(x => x.WindowLabel).HasMaxLength(128).IsRequired();
            entity.Property(x => x.TimeFrom).HasColumnType("time");
            entity.Property(x => x.TimeTo).HasColumnType("time");
            entity.HasOne(x => x.PickupHub).WithMany(h => h.DeliverySchedules).HasForeignKey(x => x.PickupHubId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(x => x.OrderNumber).IsUnique();
            entity.Property(x => x.OrderNumber).HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");
            entity.HasIndex(x => new { x.BuyerUserId, x.CreatedAtUtc });
            entity.Property(x => x.TotalAmount).HasPrecision(18, 2);
            entity.Property(x => x.DeliveryAddress).HasMaxLength(256).IsRequired();
            entity.Property(x => x.ContactEmail).HasMaxLength(256).IsRequired();
            entity.Property(x => x.ContactPhone).HasMaxLength(32).IsRequired();
            entity.HasOne(x => x.BuyerUser).WithMany(u => u.Orders).HasForeignKey(x => x.BuyerUserId);
            entity.HasOne(x => x.DeliverySchedule).WithMany(s => s.Orders).HasForeignKey(x => x.DeliveryScheduleId);
            entity.HasOne(x => x.PickupHub).WithMany(h => h.Orders).HasForeignKey(x => x.PickupHubId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(x => x.ProductName).HasMaxLength(256).IsRequired();
            entity.Property(x => x.UnitPrice).HasPrecision(18, 2);
            entity.Property(x => x.LineTotal).HasPrecision(18, 2);
            entity.HasOne(x => x.Order).WithMany(o => o.Items).HasForeignKey(x => x.OrderId);
            entity.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.OpgProfile).WithMany().HasForeignKey(x => x.OpgProfileId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<LeadForm>(entity =>
        {
            entity.HasIndex(x => x.OrderId).IsUnique();
            entity.Property(x => x.ContactEmail).HasMaxLength(256).IsRequired();
            entity.Property(x => x.ContactPhone).HasMaxLength(32).IsRequired();
            entity.Property(x => x.DeliveryAddress).HasMaxLength(256).IsRequired();
            entity.Property(x => x.Message).HasMaxLength(2000);
            entity.Property(x => x.Status).HasConversion<int>().HasDefaultValue(LeadFormStatus.Init);
            entity.HasOne(x => x.Order).WithOne(o => o.LeadForm).HasForeignKey<LeadForm>(x => x.OrderId);
            entity.HasOne(x => x.BuyerUser).WithMany().HasForeignKey(x => x.BuyerUserId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PickupHub>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(128).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(256).IsRequired();
            entity.Property(x => x.City).HasMaxLength(128).IsRequired();
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasIndex(x => new { x.ProductId, x.Status, x.ExpiresAtUtc });
            entity.HasOne(x => x.Product).WithMany(p => p.Reservations).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.BuyerUser).WithMany().HasForeignKey(x => x.BuyerUserId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.Property(x => x.Action).HasMaxLength(128).IsRequired();
            entity.Property(x => x.EntityName).HasMaxLength(128).IsRequired();
            entity.Property(x => x.Metadata).HasMaxLength(4000);
            entity.HasOne(x => x.User).WithMany(u => u.AuditLogs).HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<EmailQueueItem>(entity =>
        {
            entity.Property(x => x.ToEmail).HasMaxLength(256).IsRequired();
            entity.Property(x => x.Subject).HasMaxLength(256).IsRequired();
            entity.Property(x => x.Body).HasMaxLength(10000).IsRequired();
            entity.Property(x => x.LastError).HasMaxLength(2000);
        });

        modelBuilder.Entity<SiteSetting>(entity =>
        {
            entity.HasIndex(x => x.Key).IsUnique();
            entity.Property(x => x.Key).HasMaxLength(128).IsRequired();
            entity.Property(x => x.Value).HasMaxLength(10000).IsRequired();
        });

        modelBuilder.Entity<CmsPage>(entity =>
        {
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Slug).HasMaxLength(128).IsRequired();
            entity.Property(x => x.Title).HasMaxLength(256).IsRequired();
            entity.Property(x => x.MetaTitle).HasMaxLength(256);
            entity.Property(x => x.MetaDescription).HasMaxLength(1000);
            entity.Property(x => x.HtmlContent).HasMaxLength(20000).IsRequired();
        });
    }
}
