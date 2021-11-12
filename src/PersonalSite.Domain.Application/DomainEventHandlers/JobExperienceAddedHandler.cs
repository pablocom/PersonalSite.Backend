using PersonalSite.Domain.Events;

namespace PersonalSite.Domain.Application.DomainEventHandlers;

public class JobExperienceAddedHandler : IDomainEventHandler<JobExperienceAdded>
{
    public void Handle(JobExperienceAdded domainEvent)
    {
    }
}
