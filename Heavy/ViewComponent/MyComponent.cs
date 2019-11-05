using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Heavy
{
    public class MyComponent:ViewComponent
    {
        public async  Task<IViewComponentResult> InvokeAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://www.baidu.com");
            if (response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                return View(true);
            }
            else
            {
                return View(false);
            }
        }
    }
}
