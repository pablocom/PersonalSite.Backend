using System.Collections.Generic;
using MediatR;
using PersonalSite.Domain.Entities;

namespace PersonalSite.Server.Queries
{
    public class GetJobExperiencesQuery : IRequest<IEnumerable<JobExperience>>
    {

    }
}