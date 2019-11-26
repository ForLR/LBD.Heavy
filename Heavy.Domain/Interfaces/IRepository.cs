using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Domain.Interfaces
{
    public interface IRepository<TEntity>:IDisposable where TEntity:class 
    {
        
        
        Task Add(TEntity entity);

        Task Update(TEntity entity);

        IList<TEntity> GetAll();

        Task<TEntity> GetById(Guid id);

        IQueryable<TEntity> GetAlls(Expression<Func<TEntity, bool>> expression);

        Task Remove(Guid id);

        Task<int> SaveChanges();
    }
}
