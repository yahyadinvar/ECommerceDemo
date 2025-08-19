using ECommerceDemo.Domain.Entities.Common.Interfaces;

namespace ECommerceDemo.Domain.Entities.Common.Base;

public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity
{
    public Guid CreatedId { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? UpdatedId { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? DeletedId { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; } = false;

}