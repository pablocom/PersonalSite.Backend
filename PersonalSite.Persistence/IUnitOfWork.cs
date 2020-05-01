using System;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}