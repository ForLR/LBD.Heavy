using Heavy.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Events
{
    public class AddUserEvent:Event
    {
        public AddUserEvent(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            UserName = name;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
