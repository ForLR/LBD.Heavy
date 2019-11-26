using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Identity.Auth
{
    public class ReadHandler : AuthorizationHandler<ReadAuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadAuthRequirement requirement)
        {
            var mvcFilterContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            var actionName = mvcFilterContext.RouteData.Values["action"];
            var controllerName = mvcFilterContext.RouteData.Values["controller"];
            var claims = context.User.Claims.Where(x => x.Type == "Read").ToList();
            var readAuth = string.Format("{0}:{1}", controllerName, actionName);
            if (claims!=null&&claims.Any())
            {
                if (claims.Any(x=>x.Value== readAuth))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
