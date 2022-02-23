namespace PersonalSite.IoC;

public static class DependencyInjectionContainer
{
    private static IServiceProviderProxy? serviceProviderProxy;
    public static IServiceProviderProxy Current => serviceProviderProxy ?? throw new Exception("You should Initialize the ServiceProvider before using it.");

    public static void Init(IServiceProviderProxy proxy)
    {
        serviceProviderProxy = proxy;
    }
}

public interface IServiceProviderProxy
{
    object GetService(Type type);
}