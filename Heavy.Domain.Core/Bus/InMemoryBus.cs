using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Heavy.Domain.Core.Commands;
using Heavy.Domain.Core.Events;
using MediatR;

namespace Heavy.Domain.Core.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;
        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            this._eventStore = eventStore;
            this._mediator = mediator;
        }
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
            {
                _eventStore?.Save(@event);
            }
            return _mediator.Publish(@event);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
