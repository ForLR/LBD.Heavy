using AutoMapper;
using Heavy.Application.Interfaces;
using Heavy.Application.ViewModels.Users;
using Heavy.Domain.Core.Bus;
using Heavy.Identity.Commands;
using Heavy.Identity.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Heavy.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _user;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;


        public UserAppService(UserManager<User> user, IMediatorHandler mediator, IMapper mapper)
        {
            this._user = user;
            this._mediator = mediator;
            this._mapper = mapper;
        }
        public void DeleteAsync(string id)
        {
          
            //var user =await _user.FindByIdAsync(id);
            //if (user==null)
            //{
            //    throw new NotImplementedException();
            //}
            //await  _user.DeleteAsync(user);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Register(UserViewModel user)
        {
            var registerCommand = _mapper.Map<RegisterUserCommand>(user);
            _mediator.SendCommand(registerCommand);
        }

        public void Update(UserViewModel user)
        {
            var updateCommand = _mapper.Map<UpdateUserCommand>(user);
            _mediator.SendCommand(updateCommand);
        }
    }
}
