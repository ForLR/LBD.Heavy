using Heavy.Domain.Core.Bus;
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
    public class UserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
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
            var result = _user.CreateAsync(user);
            if (result.Result.Succeeded)
            {
                _bus.RaiseEvent(new AddUserEvent(request.AggregateId, request.UserName, request.Email, DateTime.Now));
            }
            return Task.FromResult(true);
        }
    }
}
