using Heavy.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Commands
{
    public class AddUserCommand : Command
    {
        public AddUserCommand(string userName,string email)
        {
            this.UserName = userName;
            this.Email = email;
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public override bool IsValid()
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
