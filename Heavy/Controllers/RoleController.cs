using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heavy.Models;
using Heavy.Identity.Model;

namespace Heavy.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _roleManager.Roles.ToListAsync();
            return View(result);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleAddModel addModel)
        {
            if (!ModelState.IsValid)
                return View(addModel);
            var role = new IdentityRole
            {
                Name=addModel.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }
            return View(addModel);
        }

        
        public async Task<IActionResult> EditRole(string id)
        {
            var role =await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var roleModel = new RoleEditModel
            {
                Id=id,
                RoleName=role.Name,
                Users=new List<string>()
            };

                var user =await _userManager.Users.ToListAsync();
                foreach (var item in user)
                {
                    if (await _userManager.IsInRoleAsync(item,role.Name))
                    {
                        roleModel.Users.Add(item.UserName);
                    }
                }
            return View(roleModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditModel editModel)
        {
            var role = await _roleManager.FindByIdAsync(editModel.Id);
            if (role!=null)
            {
                role.Name = editModel.RoleName;
                var result =await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"更新角色出错");
                return View(editModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"删除角色出错");
            }
            ModelState.AddModelError(string.Empty, "角色未找到");
            return View("Index");
        }

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return RedirectToAction("Index");
            var vm = new RoleUserViewModel
            {
                RoleId = role.Id
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var item in users)
            {
                if (!await _userManager.IsInRoleAsync(item,role.Name))
                {
                    vm.Users.Add(item);
                }
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(RoleUserViewModel roleUserView)
        {
            var role = await _roleManager.FindByIdAsync(roleUserView.RoleId);
            var user = await _userManager.FindByIdAsync(roleUserView.UserId);
            if (role!=null&&user!=null)
            {
                var result =await _userManager.AddToRoleAsync(user,role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "添加角色用户出错");
            }
            ModelState.AddModelError(string.Empty, "用户或者角色未找到");
            return View(roleUserView);
        }

        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return RedirectToAction("Index");
            var vm = new RoleUserViewModel
            {
                RoleId = role.Id
            };
            var users = await _userManager.Users.ToListAsync();
            foreach (var item in users)
            {
                if (await _userManager.IsInRoleAsync(item, role.Name))
                {
                    vm.Users.Add(item);
                }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(RoleUserViewModel roleUserView)
        {
            var role = await _roleManager.FindByIdAsync(roleUserView.RoleId);
            var user = await _userManager.FindByIdAsync(roleUserView.UserId);
            if (role != null && user != null)
            {
                if (!await _userManager.IsInRoleAsync(user,role.Name))
                {
                    ModelState.AddModelError(string.Empty, "用户不在角色里");
                    return View(roleUserView);
                }
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "删除角色用户出错");
            }
            ModelState.AddModelError(string.Empty, "用户或者角色未找到");
            return View(roleUserView);
        }



    }
}