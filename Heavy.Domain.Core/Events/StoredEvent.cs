using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Events
{
    /// <summary>
    /// sql 存储事件
    /// </summary>
    public class StoredEvent:Event
    {
        public StoredEvent(Event @event,string data,string user)
        {
            this.AggregateId = @event.AggregateId;
            this.Data = data;
            this.Id = Guid.NewGuid();
            this.MessageType = @event.MessageType;
            this.User = user;
        }
        protected StoredEvent()
        {

        }
        public Guid Id { get; private set; }

        public string Data { get;private set; }

        public string User { get; private set; }
    }
}
