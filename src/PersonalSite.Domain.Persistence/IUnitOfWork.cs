using System;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}