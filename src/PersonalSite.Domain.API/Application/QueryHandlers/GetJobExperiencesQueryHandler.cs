using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.API.Application.Queries;
using PersonalSite.Domain.Dtos;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Domain.Services;

namespace PersonalSite.Domain.API.Application.QueryHandlers
{
    public class GetJobExperiencesQueryHandler : IRequestHandler<GetJobExperiencesQuery, IEnumerable<JobExperienceDto>>
    {
        private readonly IJobExperienceService jobExperienceService;

        public GetJobExperiencesQueryHandler(IJobExperienceService jobExperienceService)
        {
            this.jobExperienceService = jobExperienceService;
        }

        public async Task<IEnumerable<JobExperienceDto>> Handle(GetJobExperiencesQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => jobExperienceService.GetJobExperiences(), cancellationToken);
        }
    }
}