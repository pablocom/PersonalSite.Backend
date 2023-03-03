using System.Collections.Generic;
using MediatR;
using PersonalSite.Application.Dtos;

namespace PersonalSite.Application.Queries;

public class GetJobExperiencesQuery : IRequest<IEnumerable<JobExperienceDto>>
{ }
