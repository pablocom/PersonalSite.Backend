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
    private readonly IJobExperienceService _jobExperienceService;

    public GetJobExperiencesQueryHandler(IJobExperienceService jobExperienceService)
    {
        _jobExperienceService = jobExperienceService;
    }

    public Task<IEnumerable<JobExperienceDto>> Handle(GetJobExperiencesQuery request, CancellationToken cancellationToken)
    {
        var jobExperiences = _jobExperienceService.GetJobExperiences();
        return Task.FromResult(jobExperiences);
    }
}
