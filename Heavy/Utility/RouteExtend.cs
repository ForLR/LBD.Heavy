using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Utility
{
    public static class RouteExtend
    {

       public static void UseMapGetDefault(this IEndpointRouteBuilder endpoint)
       {

            endpoint.MapGet("/hello/{name:alpha}", async context=> 
            {
                var routeName = context.Request.RouteValues["name"];
                await context.Response.WriteAsync($"{routeName}：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                //return c;
            });
       }
    }
}
