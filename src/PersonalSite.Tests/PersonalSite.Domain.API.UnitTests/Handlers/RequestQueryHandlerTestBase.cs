using System.Threading;
using MediatR;
using NUnit.Framework;

namespace PersonalSite.API.UnitTests.Handlers
{
    [TestFixture]
    public abstract class RequestQueryHandlerTestBase<TRequestHandler, TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TRequestHandler : IRequestHandler<TRequest, TResponse>
    {
        protected TRequestHandler Handler { get; private set; }
        
        [SetUp]
        protected void SetUp()
        {
            this.AdditionalSetup();
            
            Handler = this.GetRequestHandler();
        }

        protected TResponse WhenHandingRequest(TRequest request)
        {
            return Handler.Handle(request, new CancellationToken()).GetAwaiter().GetResult();
        }
        
        protected abstract TRequestHandler GetRequestHandler();
        protected virtual void AdditionalSetup() { }
    }
}