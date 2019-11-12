using Heavy.Data.Context;
using Heavy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HeavyContext _context;

        public UnitOfWork(HeavyContext context)
        {
            _context = context;
        }

        public void BeginTransation()
        {
            _context.Database.BeginTransaction();
        }

        public bool Commit()
        {
           return _context.SaveChanges()>0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
