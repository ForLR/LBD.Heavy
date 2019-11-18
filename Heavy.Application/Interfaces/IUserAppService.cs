using Heavy.Application.ViewModels.Users;
using Heavy.Identity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Application.Interfaces
{
    public interface IUserAppService: IDisposable
    {
        Task<List<UserViewModel>> AllViewModel();
        Task<User> GetById(string id);
        void Register(UserViewModel user);

        void Update(UserViewModel user);

        void DeleteAsync(string id);


    }
}
