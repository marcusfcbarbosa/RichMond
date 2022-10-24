using Richmond.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Richmond.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        void Create(TEntity entity);
        bool Delete(TEntity entity);
        void Delete(int id);
        void Edit(TEntity entity);

        TEntity GetById(int id);
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
        void SaveChanges();

        Task CreateAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
