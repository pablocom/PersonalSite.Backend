using System.Collections.Generic;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using PersonalSite.Domain.API.Application.Queries;
using PersonalSite.Domain.API.Application.QueryHandlers;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.API.UnitTests.Handlers
{
    public class WhenHandlingGetAllJobExperiencesQuery : RequestQueryHandlerTestBase<GetJobExperiencesQueryHandler,
        GetJobExperiencesQuery, IEnumerable<JobExperience>>
    {
        private IJobExperienceService service;

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

        protected override GetJobExperiencesQueryHandler GetRequestHandler()
        {
            return new GetJobExperiencesQueryHandler(service);
        }
    }
}