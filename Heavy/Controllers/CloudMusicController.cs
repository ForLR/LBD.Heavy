using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Application.ViewModels;
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

            return View(new List< CloundMusicViewModel>());
        }
    }
}