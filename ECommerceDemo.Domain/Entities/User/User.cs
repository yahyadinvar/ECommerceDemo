using ECommerceDemo.Domain.Entities.Common.Base;

namespace ECommerceDemo.Domain.Entities.User;

public partial class User : AuditableEntity<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; } = "User";

    public User()
    {
        Id = Guid.NewGuid();
    }

    public User(string firstName, string lastName, string email, string passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }
}