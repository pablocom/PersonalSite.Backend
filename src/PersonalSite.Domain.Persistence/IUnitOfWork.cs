using System;

namespace PersonalSite.Persistence;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();
}
