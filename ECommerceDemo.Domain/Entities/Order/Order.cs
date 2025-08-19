using ECommerceDemo.Domain.Entities.Common.Base;

namespace ECommerceDemo.Domain.Entities.Order;

public partial class Order : AuditableEntity<Guid>
{
    public string UserId { get; private set; } = string.Empty;
    public string ProductId { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public int PaymentMethod { get; private set; }
}
