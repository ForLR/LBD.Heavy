using Heavy.Domain.Core.Bus;
using Heavy.Domain.Core.Commands;
using Heavy.Domain.Core.Notifications;
using Heavy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationEventHandler _notification;
        private readonly IUnitOfWork _unitOfWork;
        public CommandHandler(IMediatorHandler bus, DomainNotificationEventHandler notificationEventHandler, IUnitOfWork unitOfWork)
        {
            this._bus = bus;
            this._notification = notificationEventHandler;
            this._unitOfWork = unitOfWork;
        }

        protected void NotificationErrors(Command command)
        {
            foreach (var item in command.validationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotificationEvent(command.MessageType, item.ErrorMessage));
            }
        }
        public bool Commit()
        {
            if (_notification.HasNotofications()) return false;
            if (_unitOfWork.Commit()) return true;
            _bus.RaiseEvent(new DomainNotificationEvent("Commit", "提交数据保存出错"));
            return false;
        }
    }
}
