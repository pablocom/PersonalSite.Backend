using System;

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
            this.context.SaveChanges();
            this.context.Dispose();
        }
    }
}