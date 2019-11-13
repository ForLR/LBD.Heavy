using AutoMapper;
using Heavy.Application.ViewModels.Users;
using Heavy.Identity.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Application.AutoMappers
{
    public class UserViewModelMappingProfile:Profile
    {
        public UserViewModelMappingProfile()
        {
            this.CreateMap<UserViewModel, RegisterUserCommand>().ConstructUsing(x=>new RegisterUserCommand(x.UserName,x.Password,x.Email,x.IDCard,x.Url));
        }
    }
}
