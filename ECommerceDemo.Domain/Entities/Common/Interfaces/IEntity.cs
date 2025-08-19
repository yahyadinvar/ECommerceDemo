namespace ECommerceDemo.Domain.Entities.Common.Interfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}

