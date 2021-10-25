using System.Collections.Generic;
using MediatR;
using PersonalSite.Domain.Application.Dtos;

namespace PersonalSite.Domain.API.Queries
{
    public class GetJobExperiencesQuery : IRequest<IEnumerable<JobExperienceDto>>
    {

    }
}