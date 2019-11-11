using Heavy.Data.Repository.EvenSourcing;
using Heavy.Domain.Core.Events;
using Heavy.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data.EventSourcing
{
    /// <summary>
    /// sql操作记录
    /// </summary>
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository,IUser user)
        {
            this._eventStoreRepository = eventStoreRepository;
            this._user = user;
        }

        public void Save<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new StoredEvent(@event,serializedData,_user.Name);

            _eventStoreRepository.Add(storedEvent);
        }
    }
}
