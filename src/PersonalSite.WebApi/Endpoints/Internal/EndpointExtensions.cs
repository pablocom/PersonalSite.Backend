using System.Reflection;

namespace PersonalSite.WebApi.Endpoints.Internal;

public static class EndpointExtensions
{
    public static void UseEndpoints<TAssemblyMarker>(this IApplicationBuilder app)
        => app.UseEndpoints(typeof(TAssemblyMarker));

    public static void UseEndpoints(this IApplicationBuilder app, Type assemblyMarker)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(assemblyMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, new object[] { app });
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type assemblyMarker)
    {
        return assemblyMarker.Assembly.DefinedTypes
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IEndpoints).IsAssignableFrom(t));
    }
}
