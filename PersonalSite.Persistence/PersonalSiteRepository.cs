using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public class PersonalSiteRepository : IPersonalSiteRepository
    {
        private readonly PersonalSiteDbContext context;

        public PersonalSiteRepository(PersonalSiteDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return this.context.Set<TEntity>().AsQueryable();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.context.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveEntitiesAsync();
        }
    }
}