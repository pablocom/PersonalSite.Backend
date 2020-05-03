using PersonalSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalSite.Persistence
{
    public class PersonalSiteRepository : IPersonalSiteRepository
    {
        private readonly PersonalSiteDbContext context;

        public PersonalSiteRepository(PersonalSiteDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return this.context.Set<TEntity>().AsQueryable();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            this.context.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            this.context.Set<TEntity>().RemoveRange(entities);
        }
    }
}