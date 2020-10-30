using System.Collections.Generic;
using MediatR;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.Domain.API.Application.Queries
{
    public class GetJobExperiencesQuery : IRequest<IEnumerable<JobExperience>>
    {

    }
}