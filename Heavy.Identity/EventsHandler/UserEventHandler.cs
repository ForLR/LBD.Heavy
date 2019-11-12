using Heavy.Identity.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heavy.Identity.EventsHandler
{
    public class UserEventHandler : INotificationHandler<AddUserEvent>
    {
        public Task Handle(AddUserEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }


}
