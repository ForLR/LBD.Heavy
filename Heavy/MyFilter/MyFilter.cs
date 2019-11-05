using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy
{
    public class MyFilter : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get;}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("before");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("after");
        }
    }
}
