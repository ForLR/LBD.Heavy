using Heavy.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data.Repository.EvenSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;
        public EventStoreRepository(EventStoreContext context)
        {
            this._context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
