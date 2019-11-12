using Heavy.Domain.Core.Bus;
using Heavy.Identity.Commands;
using Heavy.Identity.Events;
using Heavy.Repositorys;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heavy.Identity.CommandHandler
{
    public class UserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly UserRepository userRepository;
        private readonly IMediatorHandler _bus;
        public UserCommandHandler(UserRepository userRepository, IMediatorHandler bus)
        {
            this.userRepository = userRepository;
            this._bus = bus;
        }
        public Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // _bus.RaiseEvent();
            //userRepository.Add(new Model.User { });
            _bus.RaiseEvent(new AddUserEvent(request.AggregateId,request.UserName,request.Email,DateTime.Now));
            return Task.FromResult(true);
        }
    }
}
