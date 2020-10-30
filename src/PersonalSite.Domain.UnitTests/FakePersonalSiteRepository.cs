using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Entities;
using PersonalSite.Persistence;

namespace PersonalSite.Domain.UnitTests
{
    public class FakePersonalSiteRepository : IPersonalSiteRepository
    {
        private PersonalSiteFakeDbContext dbContext;

        public FakePersonalSiteRepository(PersonalSiteFakeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            dbContext.Add(entity);
        }

        public void AddAll<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}