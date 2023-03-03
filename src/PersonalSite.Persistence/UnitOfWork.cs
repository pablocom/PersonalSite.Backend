using Microsoft.EntityFrameworkCore.Storage;
using PersonalSite.Application;

namespace PersonalSite.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly PersonalSiteDbContext _context;
    private readonly IDbContextTransaction _dbTransaction;

    public UnitOfWork(PersonalSiteDbContext context)
    {
        _context = context;

        _dbTransaction = context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.SaveChanges();
        _dbTransaction.Commit();
    }

    public void Rollback()
    {
        _dbTransaction.Rollback();
    }

    public void Dispose()
    {
        _dbTransaction.Dispose();
        _context.Dispose();
    }
}
