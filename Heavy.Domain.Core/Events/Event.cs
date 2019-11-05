using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Events
{
    public abstract class Event:Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}
