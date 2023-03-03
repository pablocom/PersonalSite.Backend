using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Application.Dtos;
using PersonalSite.Application.Queries;

namespace PersonalSite.Application.QueryHandlers;

public class GetJobExperiencesQueryHandler : IRequestHandler<GetJobExperiencesQuery, IEnumerable<JobExperienceDto>>
{
    private readonly IJobExperienceService _jobExperienceService;

    public GetJobExperiencesQueryHandler(IJobExperienceService jobExperienceService)
    {
        _jobExperienceService = jobExperienceService;
    }

    public async Task<IEnumerable<JobExperienceDto>> Handle(GetJobExperiencesQuery request, CancellationToken cancellationToken)
    {
        var jobExperiences = await _jobExperienceService.GetJobExperiences();
        return jobExperiences;
    }
}
