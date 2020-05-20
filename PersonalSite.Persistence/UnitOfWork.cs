using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonalSiteDbContext context;
        
        public UnitOfWork(PersonalSiteDbContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}