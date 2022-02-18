namespace PersonalSite.IoC;

public static class DependencyInjectionContainer
{
    public static IServiceProvider Current => _instance;
    private static IServiceProvider _instance = null!;
    
    public static void Init(IServiceProvider serviceProvider)
    {
        _instance = serviceProvider;
    }
}