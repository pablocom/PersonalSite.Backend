namespace PersonalSite.Tests.UnitTests.Controllers
{
    public abstract class ControllerTestBase<TController>
    {
        public abstract TController GetController();
    }
}