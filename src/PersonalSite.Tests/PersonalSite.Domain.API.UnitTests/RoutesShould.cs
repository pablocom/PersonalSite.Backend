using Moq;
using MyTested.AspNetCore.Mvc;
using NUnit.Framework;
using PersonalSite.WebApi.Controllers;
using PersonalSite.WebApi.Dtos;

namespace PersonalSite.API.UnitTests
{
    [TestFixture]
    public class RoutesShould
    {
        [Test]
        public void MapToHealthCheck()
        {
            MyMvc
                .Routing()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Get)
                    .WithLocation("/HealthCheck"))
                .To<HealthCheckController>();
        }

        [Test]
        public void MapToGetAllJobExperience()
        {
            MyMvc
                .Routing()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Get)
                    .WithLocation("/JobExperience"))
                .To<JobExperienceController>();
        }

        [Test]
        public void MapToCreateJobExperience()
        {
            MyMvc
                .Routing()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("/JobExperience"))
                .To<JobExperienceController>(x => x.Create(It.IsAny<CreateJobExperienceDto>()));
        }
    }
}
