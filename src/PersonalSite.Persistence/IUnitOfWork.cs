using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}