using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;
using PersonalSite.Server.Queries;

namespace PersonalSite.Server.Handlers
{
    public class GetJobExperiencesQueryHandler : IRequestHandler<GetJobExperiencesQuery, IEnumerable<JobExperience>>
    {
        private readonly IPersonalSiteRepository repository;

        public GetJobExperiencesQueryHandler(IPersonalSiteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<JobExperience>> Handle(GetJobExperiencesQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAll<JobExperience>();
        }
    }
}