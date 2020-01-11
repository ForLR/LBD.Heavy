using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Heavy.Models;
using Heavy.Identity.Model;
using LBD.Infrastructure;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Extensions.Caching.Distributed;

namespace Heavy.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<User> _signIn;
        private readonly IDistributedCache _cache;

        public AccountController(SignInManager<User> signIn, IDistributedCache cache)
        {
            _signIn = signIn;
            this._cache = cache;
        }

        public IActionResult VerityCode()
        {
            var bitmp = VerifyCodeHelper.CreateVerifyCode(out string code);
            _cache.SetString(HttpContext.Request.Host.Host, code, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60)));
            MemoryStream memoryStream = new MemoryStream();
            bitmp.Save(memoryStream, ImageFormat.Gif);
            return File(memoryStream.ToArray(),"image/gif");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {

            if (!(userLogin.VerrityCode).Equals(_cache.GetString(HttpContext.Request.Host.Host)))
            {
                ModelState.AddModelError(string.Empty, "验证码错误");
                return View(userLogin); 
            }
            var result = await _signIn.PasswordSignInAsync(userLogin.UserName, userLogin.Password, true, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
            {
                ModelState.AddModelError(string.Empty, "账号或密码不对");
                return View(userLogin);
            }

        }

        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous()]

        public IActionResult AccessDenied([FromQuery] string ReturnUrl)
        {
           
            if (string.IsNullOrWhiteSpace(ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
            //ReturnUrl = ReturnUrl.Replace(@"/", string.Empty);
            // return RedirectToAction("Index", ReturnUrl);
        }
    }
}