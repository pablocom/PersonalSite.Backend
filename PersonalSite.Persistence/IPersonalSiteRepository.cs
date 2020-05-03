using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Entities;

namespace PersonalSite.Persistence
{
    public interface IPersonalSiteRepository
    {
        void Add<TEntity>(TEntity entity) where TEntity : Entity;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : Entity;
        void Delete<TEntity>(TEntity entity) where TEntity : Entity;
        void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
    }
}