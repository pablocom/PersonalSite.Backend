using System.Threading;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Persistence;

namespace PersonalSite.API.UnitTests.Handlers;

[TestFixture]
public abstract class RequestQueryHandlerTestBase<TRequestHandler, TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TRequestHandler : IRequestHandler<TRequest, TResponse>
{
    protected readonly IUnitOfWork UnitOfWork = Substitute.For<IUnitOfWork>();
    protected TRequestHandler Handler { get; private set; }

    [SetUp]
    protected void SetUp()
    {
        AdditionalSetup();

        Handler = GetRequestHandler();
    }

    protected TResponse WhenHandingRequest(TRequest request)
    {
        return Handler.Handle(request, new CancellationToken()).GetAwaiter().GetResult();
    }

    protected abstract TRequestHandler GetRequestHandler();
    protected virtual void AdditionalSetup() { }
}
