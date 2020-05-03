namespace PersonalSite.Tests.UnitTests
{
    public abstract class ControllerTestBase<TController>
    {
        public abstract TController GetController();
    }
}