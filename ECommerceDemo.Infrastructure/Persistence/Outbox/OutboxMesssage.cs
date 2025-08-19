namespace ECommerceDemo.Infrastructure.Persistence.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public DateTime OccurredOn { get; set; }
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
    public bool IsPublished { get; set; }
    public DateTime? PublishedOn { get; set; }
}

