using System.Collections.Generic;
using MediatR;
using PersonalSite.Application.Dtos;

namespace PersonalSite.WebApi.Queries;

public class GetJobExperiencesQuery : IRequest<IEnumerable<JobExperienceDto>>
{ }
