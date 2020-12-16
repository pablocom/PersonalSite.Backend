using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.API.Application.Queries;
using PersonalSite.Domain.Model.JobExperienceAggregate;
using PersonalSite.Domain.Services;

namespace PersonalSite.Domain.API.Application.QueryHandlers
{
    public class GetJobExperiencesQueryHandler : IRequestHandler<GetJobExperiencesQuery, IEnumerable<JobExperience>>
    {
        private readonly IJobExperienceService repository;

        public GetJobExperiencesQueryHandler(IJobExperienceService repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<JobExperience>> Handle(GetJobExperiencesQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => repository.GetJobExperiences(), cancellationToken);
        }
    }
}