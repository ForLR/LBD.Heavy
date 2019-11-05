using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Heavy.Models;
using Heavy.Identity.Model;

namespace Heavy.Controllers
{
    //[Authorize(Policy = "仅限lurui")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _user;
        private readonly IMemoryCache _memoryCache;
        public UserController(UserManager<User> user, IMemoryCache memoryCache)
        {
            _user = user;
            _memoryCache = memoryCache;
        }
        public async Task<IActionResult> Index()
        {

            if (!_memoryCache.TryGetValue("Memory", out List<User> result))
            {
                result = await _user.Users.ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));//绝对过期时间
                _memoryCache.Set("Memory", result, cacheEntryOptions);
            }

            //result = _memoryCache.GetOrCreate("Momory", entry =>
            //{
            //    entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            //    return _user.Users.ToList();
            //});
         
            return View(result);
        }
        [AllowAnonymous]
        public IActionResult AddUser()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddModel addUser)
        {

            if (!ModelState.IsValid)
                return View(addUser);
            var user = new User
            {
                UserName = addUser.UserName,
                Email = addUser.Email,
                IDCard = addUser.IDCard,
                Url=addUser.Url,
                
                

            };
            var result = await _user.CreateAsync(user,addUser.Password);
            if (result.Succeeded)
                return RedirectToAction("Index");
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }
            return View(addUser);
        }


        public async Task<IActionResult> Edit(string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null)
                RedirectToAction("Index");
            var claims = await _user.GetClaimsAsync(user);
            var vm = new UserEditModel
            {
                Email=user.Email,
                id=user.Id,
                IDCard=user.IDCard,
                UserName=user.UserName,
                Claims= claims.Select(x => x.Value).ToList()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditModel editUser)
        {
            var user = await _user.FindByIdAsync(editUser.id);
            if (user == null)
                RedirectToAction("Index");
            user.Email = editUser.Email;
            user.IDCard = editUser.IDCard;
            user.UserName = editUser.UserName;
            var result = await _user.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, "更新用户条目出错");
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user!=null)
            {
                var result = await _user.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "删除用户出错");
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ManageClaims(string id)
        {

            List<string> AllClaimTypeList = new List<string>
            {
                "Edit Albums",
                "Edit Users",
                "Edit Roles",
                "Email"
            };
            var user = await _user.Users.Include(x => x.Claims).FirstOrDefaultAsync(x=>x.Id==id);
            if (user == null)
                return RedirectToAction("Index");
            var claims = AllClaimTypeList.Except(user.Claims.Select(x => x.ClaimType)).ToList();
            var vm = new ManageClaimsModel
            {
                UserId = user.Id,
                AvailableClaims = claims
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> ManageClaims(ManageClaimsModel claimsModel)
        {
            var user = await _user.FindByIdAsync(claimsModel.UserId);
            if (user == null)
                RedirectToAction("Index");
            var claim = new IdentityUserClaim<string>
            {
                ClaimType = claimsModel.ClaimId,
                ClaimValue = claimsModel.ClaimId
            };
            user.Claims.Add(claim);
            var result = await _user.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Edit", new { user.Id});
            ModelState.AddModelError(string.Empty, "编辑用户Claims出错");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveClaim(string id,string claim)
        {
            var user = await _user.Users.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return RedirectToAction("Index");
            var claims = user.Claims.Where(x => x.ClaimType==claim).ToList();
            foreach (var item in claims)
            {
                user.Claims.Remove(item);
            }
            var result = await _user.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Edit", new { id });
            ModelState.AddModelError(string.Empty, "删除用户Claims出错");
            return View("ManageClaims",new {id});
        }

        #region 服务端验证
        
        [AllowAnonymous()]
        public async Task<IActionResult> UserNameExist([Bind("UserName")] string userName)
        {
            var user =await _user.FindByNameAsync(userName);
            if (user != null)
            {
                return Json("角色已经存在");
            }
            else
                return Json(true);
        }
        #endregion

    }
}