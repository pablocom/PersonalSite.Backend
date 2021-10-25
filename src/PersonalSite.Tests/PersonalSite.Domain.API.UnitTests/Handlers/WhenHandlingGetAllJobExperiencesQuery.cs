using System.Collections.Generic;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Queries;
using PersonalSite.Domain.API.QueryHandlers;
using PersonalSite.Domain.Application;
using PersonalSite.Domain.Application.Dtos;

namespace PersonalSite.API.UnitTests.Handlers
{
    public class WhenHandlingGetAllJobExperiencesQuery : RequestQueryHandlerTestBase<GetJobExperiencesQueryHandler,
        GetJobExperiencesQuery, IEnumerable<JobExperienceDto>>
    {
        private IJobExperienceService service;

        protected override GetJobExperiencesQueryHandler GetRequestHandler() => new(service);

        protected override void AdditionalSetup()
        {
            service = Substitute.For<IJobExperienceService>();
            base.AdditionalSetup();
        }

        [Test]
        public void ServiceIsCalled()
        {
            Handler.Handle(new GetJobExperiencesQuery(), new CancellationToken());

            service.Received(1).GetJobExperiences();
        }
    }
}