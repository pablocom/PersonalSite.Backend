using PersonalSite.WebApi.Events;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandler : IDomainEventHandler<JobExperienceAdded>
{
    public void Handle(JobExperienceAdded domainEvent)
    {
    }
}
