using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heavy.Domain.Interfaces
{
    interface IRepository<TEntity>:IDisposable where TEntity:class
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        TEntity GetById(Guid id);

        IQueryable<TEntity> GetAll();

        void Remove(Guid id);

        int SaveChanges();
    }
}
