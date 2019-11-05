using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Auth
{
    public class QualifiedUserRequirement: IAuthorizationRequirement
    {
        public readonly string _roleName;
        public QualifiedUserRequirement(string roleName)
        {
            _roleName = roleName;
        }
    }
}
