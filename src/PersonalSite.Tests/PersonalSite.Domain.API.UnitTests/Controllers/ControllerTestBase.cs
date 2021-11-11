using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Persistence;

namespace PersonalSite.API.UnitTests.Controllers;

[TestFixture]
public abstract class ControllerTestBase<TController>
{
    protected readonly ILogger<TController> Logger = Substitute.For<ILogger<TController>>();
    protected readonly IUnitOfWork UnitOfWork = Substitute.For<IUnitOfWork>();

    protected TController Controller { get; set; }

    [SetUp]
    public void SetUp()
    {
        AdditionalSetup();
        Controller = GetController();
    }

    protected virtual void AdditionalSetup() { }

    protected abstract TController GetController();
}
