using System;

namespace PersonalSite.Application;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();
}
