using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Notifications
{
    public class DomainNotificationEvent: Events.Event
    {
        public string DomainNotificationId { get; private set; }

        public string Key { get;private set; }

        public string Value { get; private set; }

        public int Version { get; private set; }
        public DomainNotificationEvent(string key,string value)
        {
            this.DomainNotificationId = Guid.NewGuid().ToString();
            Version = 1;
            this.Key = key;
            this.Value = value;
            
        }
    }
}
