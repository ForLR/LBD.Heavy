using Heavy.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Commands
{
    public class UpdateUserCommand : Command
    {
        public UpdateUserCommand(string id,string userName, string email, string idCard, string url)
        {
            this.Id = id;
            this.UserName = userName;
            this.Email = email;
            this.IDCard = idCard;
            this.Url = url;
        }

        public string Id { get; set; }
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
