using ECommerceDemo.Domain.Entities.Order;
using ECommerceDemo.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceDemo.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserId)
            .IsRequired();

        builder.Property(u => u.ProductId)
            .IsRequired();

        builder.Property(u => u.Quantity)
            .IsRequired();

        builder.HasIndex(u => u.PaymentMethod)
            .IsUnique();

        builder.ConfigureAuditableProperties<Order, Guid>();
    }
}
