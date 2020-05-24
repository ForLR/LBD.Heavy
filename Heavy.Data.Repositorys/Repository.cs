
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Heavy.Data.Repositorys
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>, IDisposable where TEntity : class where TContext : DbContext
    {
        protected IDbContextFactory dbContextFactory;
        protected DbContext _context { get; set; }
        protected DbSet<TEntity> db;
        public Repository(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
            _context = this.dbContextFactory.ConnWriteOrRead(WriteAndReadEnum.Write);
            db = _context.Set<TEntity>();
        }

        protected void SetWriteOrRead(WriteAndReadEnum writeAndReadEnum)
        {
            _context = this.dbContextFactory.ConnWriteOrRead(writeAndReadEnum);
            db = _context.Set<TEntity>();
        }

        public Task Add(TEntity entity)
        {
            _context = dbContextFactory.ConnWriteOrRead(WriteAndReadEnum.Read);
            return db.AddAsync(entity).AsTask();
        }

        public IList<TEntity> GetAll(WriteAndReadEnum writeAndReadEnum = WriteAndReadEnum.Read)
        {
            SetWriteOrRead(writeAndReadEnum);
            return db.ToList();
        }

        public IQueryable<TEntity> GetAlls(Expression<Func<TEntity, bool>> expression)
        {
            return db.Where(expression);
        }

        public Task<TEntity> GetById(Guid id, WriteAndReadEnum writeAndReadEnum = WriteAndReadEnum.Read)
        {
            SetWriteOrRead(writeAndReadEnum);

            return db.FindAsync(id).AsTask();
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
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }

        }

    }
}
