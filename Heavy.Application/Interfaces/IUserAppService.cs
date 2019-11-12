using Heavy.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Application.Interfaces
{
    public interface IUserAppService: IDisposable
    {
        void Register(UserViewModel user);

        void Update(UserViewModel user);

        void DeleteAsync(string id);


    }
}
