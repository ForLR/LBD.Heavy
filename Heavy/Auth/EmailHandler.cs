using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Auth
{
    public class EmailHandler : AuthorizationHandler<EmailRequirement>
    {
        
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EmailRequirement requirement
            )
        {
            var claim=context.User.Claims.FirstOrDefault(x=>x.Type=="Email");
            if (claim!=null)
            {
                if (claim.Value.EndsWith(requirement._email))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }

      
    }
}
