using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.Core.Events
{
    public interface IEventStore
    {

        void Save<T>(T @event) where T : Event;
    }
}
