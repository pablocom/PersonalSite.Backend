using PersonalSite.Domain.Events;

namespace PersonalSite.Domain.WebApi.Installers;

public static class DomainEventsInstaller
{
    public static void Init()
    {
        DomainEvents.Init();
    }
}
