using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalSite.Domain;

public interface IDomainEventPublisher
{
    Task Publish(IEnumerable<IDomainEvent> domainEvent);
}
