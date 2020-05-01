using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalSite.Persistence
{
    public interface IPersonalSiteRepository
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        Task SaveChangesAsync();
    }
}