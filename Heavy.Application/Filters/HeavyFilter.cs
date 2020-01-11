using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Application.Filters
{
    public class HeavyFilter :ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("before");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("after");
        }
    }
}
