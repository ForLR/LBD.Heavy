using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Heavy.Controllers
{
    /// <summary>
    /// 网易云音乐播放
    /// </summary>
    public class CloudMusicController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}