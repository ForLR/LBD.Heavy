using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heavy.Domain.Core.Notifications
{
    public class DomainNotificationEventHandler : INotificationHandler<DomainNotificationEvent>
    {
        private List<DomainNotificationEvent> _notifications;
        public DomainNotificationEventHandler()
        {
            _notifications = new List<DomainNotificationEvent>();
        }
        public Task Handle(DomainNotificationEvent notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotificationEvent> GetDomainNotifications() => _notifications;

        public virtual bool HasNotofications() => _notifications.Any();

        public void Dispose()=> _notifications = new List<DomainNotificationEvent>();
     
    }
}
