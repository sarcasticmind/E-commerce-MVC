using eStore.Models;
using eStore.Services;
using eStore.ViewModels;
using eStore.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<AccountUser> signInManager;
        UserManager<AccountUser> userManager;
        IReposatory<AccountUser> reposatory;
        public AccountController(UserManager<AccountUser> userManager,
            SignInManager<AccountUser> signInManager, IReposatory<AccountUser> _reposatory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            reposatory = _reposatory;
        }

        #region Register
        //registration User insert
        [HttpGet]//from anchor tag
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]//pree submit button
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            if (ModelState.IsValid == true)
            {
                //save object db
                AccountUser userModel = new AccountUser();
                userModel.UserName = newUser.Name;
                userModel.PasswordHash = newUser.Password;
                userModel.Email = newUser.Email;

                IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);//hash passwor
                if (result.Succeeded)
                {
                    //save sucess
                    await userManager.AddToRoleAsync(userModel , "customer");
                    await signInManager.SignInAsync(userModel, false);//cookie
                    return RedirectToAction("Index","Product");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(newUser);
        }
        #endregion
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (ModelState.IsValid == true)
            {
                AccountUser user = await userManager.FindByEmailAsync(loginUser.Email);
                if (user != null )
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.isPresistant, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username & password");
                }
            }
            return View(loginUser);
        }
        #region Admin Registration
        //registration admin add
        /// adding admin should be by another admin.
        [Authorize(Roles = "admin")]
        [HttpGet]//from anchor tag
        public IActionResult CreateAdmin()
        {
            return View("register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(RegisterViewModel newUser)
        {
            if (ModelState.IsValid == true)
            {
                //save object db
                AccountUser userModel = new AccountUser();
                userModel.UserName = newUser.Name;
                userModel.PasswordHash = newUser.Password;
                userModel.Email = newUser.Email;

                IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);//hash passwor
                if (result.Succeeded)
                {
                    //enroll in role
                    await userManager.AddToRoleAsync(userModel, "admin");
                    //save sucess
                    await signInManager.SignInAsync(userModel, false);//cookie
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(newUser);
        }
        #endregion

        #region adminLogin
        public IActionResult Loginadmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Loginadmin(LoginViewModel loginUser)
        {
            if (ModelState.IsValid == true)
            {
                AccountUser user = await userManager.FindByEmailAsync(loginUser.Email);
                if (user != null )
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.isPresistant, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username & password");
                }
            }
            return View(loginUser);
        }

        #endregion
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();//expires cookie
            return RedirectToAction("Login");
        }
    }
}
