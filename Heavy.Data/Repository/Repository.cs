using Heavy.Data.Context;
using Heavy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Data.Repository
{
    public class Repository<TContext,TEntity> : IRepository<TEntity> where TEntity : class where TContext:DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> db;
        public Repository(TContext context)
        {
            this._context = context;
            this.db = context.Set<TEntity>();
        }


        public Task Add(TEntity entity)
        {
            return db.AddAsync(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(Guid id)
        {
            return db.FindAsync(id);
        }

        public Task Remove(Guid id)
        {
            db.Remove(db.Find(id));
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
