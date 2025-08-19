using ECommerceDemo.Application.Abstractions.Services;
using ECommerceDemo.Domain.Entities.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceDemo.Infrastructure.Persistence.Helpers;

public class AuditFieldSetter
{
    private readonly ICurrentUserService _currentUser;

    public AuditFieldSetter(ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public void ApplyAudit(ChangeTracker changeTracker)
    {
        var userId = _currentUser.UserId;
        var now = DateTime.UtcNow;

        foreach (var entry in changeTracker.Entries<object>())
        {
            if (entry.Entity is IAuditableEntity auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.CreatedDate = now;
                        auditable.CreatedId = userId ?? Guid.Empty;
                        auditable.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        auditable.UpdatedDate = now;
                        auditable.UpdatedId = userId;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        auditable.DeletedDate = now;
                        auditable.DeletedId = userId;
                        auditable.IsDeleted = true;
                        break;
                }
            }
        }
    }
}