using Heavy.Data.Context;
using Heavy.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(StoredEvent storedEvent)
        {
            if (storedEvent==null)
            {
                throw new ArgumentNullException();
            }
            _context.Add(storedEvent);
            _context.SaveChanges();
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _context.StoredEvents.Where(x => x.AggregateId == aggregateId).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
