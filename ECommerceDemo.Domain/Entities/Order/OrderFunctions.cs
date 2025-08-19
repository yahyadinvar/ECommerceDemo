namespace ECommerceDemo.Domain.Entities.Order;

public partial class Order
{
    public Order()
    {
        this.Id = Guid.NewGuid();
    }

    public void Added(string userId, string productId, int quantity, int paymentMethod)
    {
        this.UserId = userId;
        this.ProductId = productId;
        this.Quantity = quantity;
        this.PaymentMethod = paymentMethod;

        this.AddDomainEvent(new OrderPlacedEvent(this.Id));
    }

}
