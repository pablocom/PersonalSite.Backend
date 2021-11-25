using PersonalSite.Domain.Events;

namespace PersonalSite.Application.DomainEventHandlers;

public class JobExperienceAddedHandler : IDomainEventHandler<JobExperienceCreated>
{
    public void Handle(JobExperienceCreated domainEvent)
    {
    }
}
