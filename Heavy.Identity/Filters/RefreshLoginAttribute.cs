using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Identity.Filters
{
    public class RefreshLoginAttribute: ActionFilterAttribute
    {
       

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await context.HttpContext.RefershLoginAsync();
            await next();
        }
    }

}
