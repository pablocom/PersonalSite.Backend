using Microsoft.AspNetCore.Routing;

namespace PersonalSite.WebApi.Endpoints.Internal;

public interface IEndpoints
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
}
