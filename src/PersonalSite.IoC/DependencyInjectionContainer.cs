namespace PersonalSite.IoC;

public static class DependencyInjectionContainer
{
    private static IServiceProviderProxy? _serviceProviderProxy;
    public static IServiceProviderProxy Current
    {
        get
        {
            if (_serviceProviderProxy is null)
            {
                throw new Exception("You should Initialize the ServiceProvider before using it.");
            }

            return _serviceProviderProxy;
        }
    }

    public static void Init(IServiceProviderProxy proxy)
    {
        _serviceProviderProxy = proxy;
    }
}

public interface IServiceProviderProxy
{
    object? GetService(Type type);
    TService GetRequiredService<TService>() where TService : notnull;
}