using ECommerceDemo.Domain.Entities.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceDemo.Infrastructure.Extensions;

public static class AuditableEntityConfigurationExtension
{
    public static void ConfigureAuditableProperties<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : AuditableEntity<TKey>
    {
        builder.Property(e => e.CreatedId)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.CreatedDate)
               .IsRequired()
               .HasColumnType("datetime2");

        builder.Property(e => e.UpdatedId)
               .HasMaxLength(50)
               .IsRequired(false);

        builder.Property(e => e.UpdatedDate)
               .HasColumnType("datetime2")
               .IsRequired(false);

        builder.Property(e => e.DeletedId)
               .HasMaxLength(50)
               .IsRequired(false);

        builder.Property(e => e.DeletedDate)
               .HasColumnType("datetime2")
               .IsRequired(false);

        builder.Property(e => e.IsDeleted)
               .HasDefaultValue(false)
               .IsRequired();
    }
}