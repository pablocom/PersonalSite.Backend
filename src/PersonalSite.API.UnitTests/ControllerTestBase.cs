using NUnit.Framework;

namespace PersonalSite.API.UnitTests
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