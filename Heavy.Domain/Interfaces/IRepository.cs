using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Domain.Interfaces
{
    public interface IRepository<TEntity>:IDisposable where TEntity:class
    {
        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task<TEntity> GetById(Guid id);

        Task<IQueryable<TEntity>> GetAll();

        Task Remove(Guid id);

        Task<int> SaveChanges();
    }
}
