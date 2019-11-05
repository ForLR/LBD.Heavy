using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Auth
{
    public class RoleHandler : AuthorizationHandler<QualifiedUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QualifiedUserRequirement requirement)
        {
            var role = context.User.IsInRole(requirement._roleName);
            if (role)
                context.Succeed(requirement);
           return Task.CompletedTask;
        }
    }
}
