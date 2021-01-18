using System.Collections.Generic;
using MediatR;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Domain.Services.Dtos;

namespace PersonalSite.Domain.API.Application.Queries
{
    public class GetJobExperiencesQuery : IRequest<IEnumerable<JobExperienceDto>>
    {

    }
}