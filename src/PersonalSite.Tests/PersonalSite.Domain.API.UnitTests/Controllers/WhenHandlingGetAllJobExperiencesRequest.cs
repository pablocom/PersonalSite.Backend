using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.WebApi.Controllers;
using PersonalSite.WebApi.Queries;

namespace PersonalSite.API.UnitTests.Controllers;

[TestFixture]
public class WhenHandlingGetAllJobExperiencesRequest : ControllerTestBase<JobExperienceController>
{
    private IMediator mediator;

    protected override JobExperienceController GetController() => new(mediator);

    protected override void AdditionalSetup()
    {
        mediator = Substitute.For<IMediator>();
    }

    [Test]
    public async Task GetJobExperiencesQueryIsSent()
    {
        await Controller.GetAll();

        await mediator.Received(1).Send(Arg.Is<GetJobExperiencesQuery>(x => true));
    }
}
