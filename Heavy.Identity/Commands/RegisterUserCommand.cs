using Heavy.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Commands
{
    public class RegisterUserCommand : Command
    {
        public RegisterUserCommand(string userName,string email,string idCard,string url)
        {
            this.UserName = userName;
            this.Email = email;
            this.IDCard = idCard;
            this.Url = url;
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IDCard { get; set; }

        public string Url { get; set; }
        public override bool IsValid()
        {
            return true;
        }

    }
}
