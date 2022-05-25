using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Application;
using PersonalSite.Application.Dtos;
using PersonalSite.WebApi.Queries;
using PersonalSite.WebApi.QueryHandlers;

namespace PersonalSite.API.UnitTests.Handlers;

public class WhenHandlingGetAllJobExperiencesQuery : RequestQueryHandlerTestBase<GetJobExperiencesQueryHandler,
    GetJobExperiencesQuery, IEnumerable<JobExperienceDto>>
{
    private IJobExperienceService _service;

    protected override GetJobExperiencesQueryHandler GetRequestHandler() => new(_service);

    protected override void AdditionalSetup()
    {
        _service = Substitute.For<IJobExperienceService>();
        base.AdditionalSetup();
    }

    [Test]
    public async Task ServiceIsCalled()
    {
        await Handler.Handle(new GetJobExperiencesQuery(), new CancellationToken());
        await _service.Received(1).GetJobExperiences();
    }
}
