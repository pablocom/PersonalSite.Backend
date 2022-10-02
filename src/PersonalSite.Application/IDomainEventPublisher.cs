using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Domain;

public interface IDomainEventPublisher
{
    Task PublishAsync(IEnumerable<IDomainEvent> domainEvent);
}
