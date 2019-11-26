using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Model
{
    public class ClaimType
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public ClaimTypeEnum ApplicationType { get; set; }
    }

    public enum ClaimTypeEnum
    {
        User,
        Role
    }

}
