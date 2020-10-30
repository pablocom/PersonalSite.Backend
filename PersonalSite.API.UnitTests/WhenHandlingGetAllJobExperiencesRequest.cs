using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Controllers;
using PersonalSite.Persistence;
using PersonalSite.Server.Queries;

namespace PersonalSite.API.UnitTests
{
    [TestFixture]
    public class WhenHandlingGetAllJobExperiencesRequest : ControllerTestBase<JobExperienceController>
    {
        private IMediator mediator;

        protected override void AdditionalSetup()
        {
            mediator = Substitute.For<IMediator>();
        }

        [Test]
        public async Task ServiceIsCalled()
        {
            await Controller.GetAll();

            await mediator.Received(1).Send(Arg.Is<GetJobExperiencesQuery>(x => true));
        }
        
        protected override JobExperienceController GetController()
        {
            return new JobExperienceController(Substitute.For<ILogger<JobExperienceController>>(),
                Substitute.For<IUnitOfWork>(), mediator);
        }
    }
}