using ExamForms.Constants;
using ExamForms.Manager.Accounts;
using ExamForms.Models.Accounts;
using ExamForms.Utility;
using ExamForms.ViewModel.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Security.Claims;

namespace ExamForms.Areas.Accounts.Controllers
{
    [Area(nameof(Accounts))]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AdministrationManager administrationManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController(RoleManager<ApplicationRole> roleManager
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , AdministrationManager _administrationManager
            , ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.administrationManager = _administrationManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListOfAllUsers(int? currentPage)
        {
            try
            {
                List<ApplicationUserViewModel> applicationUserViewModel = new List<ApplicationUserViewModel>();
                applicationUserViewModel = await administrationManager.GetUsersAsync();
                return View(applicationUserViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ListOfAllUsers(
            int? currentPage, string firstName, string userName, string email)
        {
            try
            {
                List<ApplicationUserViewModel> applicationUserViewModel = new List<ApplicationUserViewModel>();
                applicationUserViewModel = await administrationManager.GetUsersAsync();
                return View(applicationUserViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        //[Authorize(Policy = "CreateRolePolicy")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = new ApplicationRole
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfAllRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListOfAllRoles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(x => x.Type + " -> " + x.Value).ToList(),
                Roles = userRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {model.UserId} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfAllUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfAllUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListOfAllUsers");
            }
        }

        [HttpPost]
        //[Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRoleById(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListOfAllRoles");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("ListOfAllRoles");
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError($"Error deleting role {ex}");
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users" +
                        $"in the role. If you want to delete this role, please rempve the users from" +
                        $"the role and then try to delete";
                    return View("Error");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} cannot be found";
                return View("NotFound");
            }
            var model = new EditRoleViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {model.RoleId} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfAllRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> AssignRolesToUser(string userId)
        {
            ViewBag.UserId = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {userId} cannot be found";
                return View("NotFound");
            }
            var model = new List<AssignRolesToUserViewModel>();
            var roles = await roleManager.Roles.ToListAsync();
            foreach (var role in roles)
            {
                var assignRolesToUserViewModel = new AssignRolesToUserViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    assignRolesToUserViewModel.IsSelected = true;
                }
                else
                {
                    assignRolesToUserViewModel.IsSelected = false;
                }
                model.Add(assignRolesToUserViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddToAdmin(List<string> userIds)
        {
            var users = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            if (!users.Any() && users == null)
            {
                ViewBag.ErrorMessage = $"User with id = {userIds} cannot be found";
                return View("NotFound");
            }
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, roles);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, Enums.AppRoleEnums.Admin.ToString());
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(List<string> userIds)
        {
            bool isSameUser = false;
            var users = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            if (!users.Any() && users == null)
            {
                ViewBag.ErrorMessage = $"User with id = {userIds} cannot be found";
                return View("NotFound");
            }
            foreach (var user in users)
            {
                var result = await userManager.RemoveFromRoleAsync(user, Enums.AppRoleEnums.Admin.ToString());
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, Enums.AppRoleEnums.User.ToString());
                }
                if (user.UserName == User.Identity.Name) isSameUser = true;                    
            }
            if (isSameUser)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("SignIn", "Account", new { area = "Accounts" });
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {userId} cannot be found";
                return View("NotFound");
            }
            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var model = new UserClaimsViewModel
            {
                UserId = userId
            };
            foreach (var claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };
                if (existingUserClaims.Any(x => x.Type == claim.Type && x.Value == "True"))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {model.UserId} cannot be found";
                return View("NotFound");
            }
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user exixting claims");
                return View(model);
            }
            result = await userManager.AddClaimsAsync(user,
                model.Claims.Select(x => new Claim(x.ClaimType, x.IsSelected ? "True" : "False")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }
            return RedirectToAction("EditUser", new { Id = model.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoleByAdmin(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoleByAdmin(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditUserRole", new { id = roleId });
                    }
                }
            }
            return RedirectToAction("EditUserRole", new { id = roleId });
        }

        public IActionResult DeleteConfirmationModal(string id)
        {
            try
            {
                var modal = new DeleteModalOptions()
                {
                    Modal = "modal_confirm_delete",
                    ActionName = "DeleteUserById",
                    ControllerName = "Administration",
                    LinkText = "Delete",
                    Message = "Are you sure you want to delete this User Profile? After confirm you can not undo this action.",
                    RouteValues = id
                };
                return PartialView("_DeleteConfirmationModal", modal);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUsers(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { success = false, message = "No users selected." });
            }

            var data = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            foreach (var user in data)
            {
                var result = await userManager.DeleteAsync(user);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> BlockUsers(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { success = false, message = "No users selected." });
            }

            var data = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            foreach (var user in data)
            {
                var result = await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { success = false, message = "No users selected." });
            }

            var data = userManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            foreach (var user in data)
            {
                var result = await userManager.SetLockoutEndDateAsync(user, null);
            }
            return Json(new { success = true });
        }
    }

}
