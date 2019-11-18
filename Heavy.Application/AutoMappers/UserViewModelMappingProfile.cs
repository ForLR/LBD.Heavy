using AutoMapper;
using Heavy.Application.ViewModels.Users;
using Heavy.Identity.Commands;
using Heavy.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heavy.Application.AutoMappers
{
    public class UserViewModelMappingProfile:Profile
    {
        public UserViewModelMappingProfile()
        {
            this.CreateMap<UserViewModel, RegisterUserCommand>().ConstructUsing(x=>new RegisterUserCommand(x.UserName,x.Password,x.Email,x.IDCard,x.Url));
            this.CreateMap<UserViewModel, UpdateUserCommand>().ConvertUsing(x => new UpdateUserCommand(x.Id,x.UserName,x.Email,x.IDCard,x.Url));
            this.CreateMap<User, UserViewModel>().ConvertUsing(x => new UserViewModel { Email = x.Email, Id = x.Id, IDCard = x.IDCard, Url = x.Url, UserName = x.UserName, Claims = x.Claims.Select(c => c.ClaimValue).ToList() });
        }
    }
}
