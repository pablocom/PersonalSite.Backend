using NUnit.Framework;

namespace PersonalSite.Tests.UnitTests.Controllers
{
    [TestFixture]
    public abstract class ControllerTestBase<TController>
    {
        protected TController Controller { get; set; }

        [SetUp]
        public void SetUp()
        {
            AdditionalSetup();
            Controller = GetController();
        }

        protected virtual void AdditionalSetup() {}
        
        protected abstract TController GetController();
    }
}