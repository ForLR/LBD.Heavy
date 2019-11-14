using Heavy.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Events
{
    public class UpdateUserEvent: Event
    {
        public UpdateUserEvent(string id, string userName, string email, string idCard, string url)
        {
            this.Id = id;
            this.UserName = userName;
            this.Email = email;
            this.IDCard = idCard;
            this.Url = url;
            this.AggregateId = id;
        }
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string IDCard { get; set; }

        public string Url { get; set; }
    }
}
