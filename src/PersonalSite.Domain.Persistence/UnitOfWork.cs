using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace PersonalSite.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly PersonalSiteDbContext context;
        private readonly IDbContextTransaction dbTransaction;
        
        public UnitOfWork(PersonalSiteDbContext context)
        {
            this.context = context;

            dbTransaction = context.Database.BeginTransaction();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        public void Rollback()
        {
            dbTransaction.Rollback();
        }

        public void Dispose()
        {
            dbTransaction.Dispose();
            context.Dispose();
        }
    }
}