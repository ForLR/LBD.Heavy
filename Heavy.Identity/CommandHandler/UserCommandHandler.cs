using Heavy.Domain.Core.Bus;
using Heavy.Domain.Core.Notifications;
using Heavy.Identity.Commands;
using Heavy.Identity.Events;
using Heavy.Identity.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heavy.Identity.CommandHandler
{
    public class UserCommandHandler : IRequestHandler<RegisterUserCommand, bool>,IRequestHandler<UpdateUserCommand,bool>
    {
        
        private readonly IMediatorHandler _bus;
        private readonly UserManager<User> _user;

        public UserCommandHandler( IMediatorHandler bus, UserManager<User> user)
        {
            this._bus = bus;
            this._user = user;
        }
        public Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
         
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                IDCard = request.IDCard,
                Url = request.Url,
            };
            if ( _user.FindByNameAsync(user.UserName).Result!=null)
            {
                _bus.RaiseEvent(new DomainNotificationEvent(request.MessageType,"已存在该用户名的用户"));
                return Task.FromResult(false);
            }

            var result = _user.CreateAsync(user,request.PassoWord);
            if (result.Result.Succeeded)
            {
                _bus.RaiseEvent(new AddUserEvent(user.Id, user.UserName, user.Email, DateTime.Now));
            }
            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return Task.FromResult(false);
            }
            var user = _user.FindByIdAsync(request.Id).Result;
            if (user==null)
            {
                _bus.RaiseEvent(new DomainNotificationEvent(request.MessageType, "不存在的用户"));
                return Task.FromResult(false);
            }
            if (_user.FindByNameAsync(request.UserName).Result!=null)
            {
                if (!user.UserName.Equals(request.UserName))
                {
                    _bus.RaiseEvent(new DomainNotificationEvent(request.MessageType, "已存在该用户名的用户"));
                    return Task.FromResult(false);
                }
               
            }
            user.IDCard = request.IDCard;
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.Url = request.Url;
            var result = _user.UpdateAsync(user);
            if (result.Result.Succeeded)
            {
                _bus.RaiseEvent(new UpdateUserEvent(user.Id,user.UserName,user.Email,user.Id,user.Url)) ;
                return Task.FromResult(true);
            }
            else
            {
                foreach (var item in result.Result.Errors)
                {
                    _bus.RaiseEvent(new DomainNotificationEvent(request.MessageType,item.Description)); ;
                }
                return Task.FromResult(false);
            }
            
            
        }
    }
}
