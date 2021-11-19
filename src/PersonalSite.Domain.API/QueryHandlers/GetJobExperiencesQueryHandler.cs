using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Application;
using PersonalSite.Application.Dtos;
using PersonalSite.WebApi.Queries;

namespace PersonalSite.WebApi.QueryHandlers;

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
