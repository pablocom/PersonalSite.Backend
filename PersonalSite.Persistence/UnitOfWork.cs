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

        public void Dispose()
        {
            Task.WaitAll(this.SaveChangesAsync());
            this.context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await this.context.SaveEntitiesAsync(cancellationToken);
        }
    }
}