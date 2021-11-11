using Microsoft.EntityFrameworkCore.Storage;

namespace PersonalSite.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly PersonalSiteDbContext context;
    private readonly IDbContextTransaction dbTransaction;

    public UnitOfWork(PersonalSiteDbContext context)
    {
        this.context = context;

        dbTransaction = context.Database.BeginTransaction();
    }

    public void Commit()
    {
        context.SaveChanges();
        dbTransaction.Commit();
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
