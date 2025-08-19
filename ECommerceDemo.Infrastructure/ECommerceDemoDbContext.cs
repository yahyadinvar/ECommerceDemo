using ECommerceDemo.Domain.Entities.Common.Base;
using ECommerceDemo.Domain.Entities.Order;
using ECommerceDemo.Domain.Entities.User;
using ECommerceDemo.Infrastructure.Extensions;
using ECommerceDemo.Infrastructure.Persistence.Helpers;
using ECommerceDemo.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace ECommerceDemo.Infrastructure;

public class ECommerceDemoDbContext : DbContext
{
    private readonly AuditFieldSetter _auditor;
    public ECommerceDemoDbContext(DbContextOptions<ECommerceDemoDbContext> options, AuditFieldSetter auditor) : base(options)
    {
        _auditor = auditor;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _auditor.ApplyAudit(ChangeTracker);
        this.AddDomainEventsToOutbox();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDemoDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<DomainEvent>();
    }
}
