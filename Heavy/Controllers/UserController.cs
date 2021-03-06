﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heavy.Models;
using Heavy.Identity.Model;
using Heavy.Application.Interfaces;
using Heavy.Application.ViewModels.Users;
using MediatR;
using Heavy.Domain.Core.Notifications;
using Heavy.Identity.Repositorys;
using Heavy.Identity.Filters;

namespace Heavy.Controllers
{
    //[Authorize(Policy = "仅限lurui")]
    //[Authorize(Policy = "ReadAuth")]
    [RefreshLogin]
    [Authorize(Policy = "仅限lurui")]
   
    public class UserController : Controller
    {
        private readonly UserManager<User> _user;
        private readonly DomainNotificationEventHandler _notification;
        private readonly ClaimTypeRepository _claimTypeRepository;
        private readonly IUserAppService _userAppService;


        public UserController(UserManager<User> user, IUserAppService userAppService, INotificationHandler<DomainNotificationEvent> notification, ClaimTypeRepository claimTypeRepository)
        {
            _user = user;
            this._userAppService= userAppService;
            this._notification = notification as DomainNotificationEventHandler;
            this._claimTypeRepository = claimTypeRepository;
        }

      
        public async Task<IActionResult> Index()
        {

            var result = await _userAppService.AllViewModel();


            return View(result);
        }
        [AllowAnonymous]
        public IActionResult AddUser()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public  IActionResult AddUser(UserViewModel addUser)
        {
         
            if (!ModelState.IsValid)
                return View(addUser);
            _userAppService.Register(addUser);

            if (!_notification.HasNotofications())
            {
                return RedirectToAction("Index");
            }
            foreach (var item in _notification.GetDomainNotifications())
            {
                ModelState.AddModelError(string.Empty, item.Value);
            }
            return View(addUser);
        }


        public async Task<IActionResult> Edit(string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null)
                RedirectToAction("Index");
            var claims = await _user.GetClaimsAsync(user);
            var vm = new UserViewModel
            {
                Email=user.Email,
                Id=user.Id,
                IDCard=user.IDCard,
                UserName=user.UserName,
                Url=user.Url,
                Claims= claims.Select(x => x.Value).ToList()
            };
         
            return View(vm);
        }
        [HttpPost]
        public  IActionResult Edit(UserViewModel editUser)
        {
            if (!ModelState.IsValid)
                return View(editUser);
            _userAppService.Update(editUser);
            if (!_notification.HasNotofications())
            {
                return RedirectToAction("Index");
            }
            foreach (var item in _notification.GetDomainNotifications())
            {
                ModelState.AddModelError(string.Empty, item.Value);
            }
            return View(editUser);
        }

        public IActionResult Delete(string id)
        {
            _userAppService.DeleteAsync(id);
            if (!_notification.HasNotofications())
            {
                return RedirectToAction("Index");
            }
            foreach (var item in _notification.GetDomainNotifications())
            {
                ModelState.AddModelError(string.Empty, item.Value);
            }

          
            return RedirectToAction("Index");
        }

        public IActionResult AddClaims()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClaims(ClaimTypeViewModel claimType)
        {

            await _claimTypeRepository.Add(new ClaimType {ApplicationType= ClaimTypeEnum.User,Name= claimType.Name });
            await _claimTypeRepository.SaveChanges();
            return View(model: claimType);
        }
        public async Task<IActionResult> ManageClaims(string id)
        {
            var claim = _claimTypeRepository.GetAlls(x=>x.ApplicationType== ClaimTypeEnum.User);
            List<string> AllClaimTypeList = claim.Select(x => x.Name).ToList();
            //List<string> AllClaimTypeList = new List<string>
            //{
            //    "Edit Albums",
            //    "Edit Users",
            //    "Edit Roles",
            //    "Email"
            //};
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
        [RefreshLogin]
        public async Task<IActionResult> ManageClaims(ManageClaimsModel claimsModel)
        {
            var user = await _userAppService.GetById(claimsModel.UserId);
            if (user == null)
                RedirectToAction("Index");
            var claim = new IdentityUserClaim<string>
            {
                ClaimType = claimsModel.ClaimId,
                ClaimValue = claimsModel.ClaimValue,
                UserId=user.Id
            };
            user.Claims.Add(claim);
            var result = await _user.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Edit", new { user.Id});
            ModelState.AddModelError(string.Empty, "编辑用户Claims出错");
            
            return View(user);
        }
        [HttpPost]
        [RefreshLogin]

        public async Task<IActionResult> RemoveClaim(string id,string claim)
        {
            var user = await _user.Users.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return RedirectToAction("Index");
            var claims = user.Claims.Where(x => x.ClaimValue==claim).ToList();
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