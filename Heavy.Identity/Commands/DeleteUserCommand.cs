using Heavy.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Commands
{
    public class DeleteUserCommand:Command
    {
        public DeleteUserCommand(string id)
        {
            this.Id = id;
        }
        public string Id { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
