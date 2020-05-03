using System;
using Microsoft.Extensions.Logging;

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
            try
            {
                this.context.SaveChanges();
                this.context.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}