
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Heavy.Data.Repositorys
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {


        Task Add(TEntity entity);

        Task Update(TEntity entity);

        IList<TEntity> GetAll(WriteAndReadEnum writeAndReadEnum);

        Task<TEntity> GetById(Guid id, WriteAndReadEnum writeAndReadEnum);

        IQueryable<TEntity> GetAlls(Expression<Func<TEntity, bool>> expression);

        Task Remove(Guid id);

        Task<int> SaveChanges();


    }
}
