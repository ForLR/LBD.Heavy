using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heavy.Identity.Filters
{
    public static class HttpContextExtensions
    {

        public static async Task RefershLoginAsync(this HttpContext httpContext)
        {
            if (httpContext.User == null) return;
            var signInManage= httpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
            if (!signInManage.IsSignedIn(httpContext.User))
            {
                var userManage = httpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                var user = await userManage.GetUserAsync(httpContext.User);
                await signInManage.RefreshSignInAsync(user);
            }
        }
    }
}
