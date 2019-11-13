using Heavy.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data.Repository.EvenSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Add(StoredEvent storedEvent);
        IList<StoredEvent> All(string aggregateId);
    }
}
