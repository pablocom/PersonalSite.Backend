using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.API.Queries;
using PersonalSite.Domain.Application;
using PersonalSite.Domain.Application.Dtos;

namespace PersonalSite.Domain.API.QueryHandlers
{
    public class GetJobExperiencesQueryHandler : IRequestHandler<GetJobExperiencesQuery, IEnumerable<JobExperienceDto>>
    {
        private readonly IJobExperienceService jobExperienceService;

        public GetJobExperiencesQueryHandler(IJobExperienceService jobExperienceService)
        {
            this.jobExperienceService = jobExperienceService;
        }

        public Task<IEnumerable<JobExperienceDto>> Handle(GetJobExperiencesQuery request, CancellationToken cancellationToken)
        {
            var jobExperiences = jobExperienceService.GetJobExperiences();
            return Task.FromResult(jobExperiences);
        }
    }
}