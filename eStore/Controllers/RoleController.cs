using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eStore.Controllers
{
   //[Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> New(string RoleName)
        {
            if (RoleName != null)
            {
                RoleName = RoleName.Trim().ToLower();
                IdentityRole role = new IdentityRole(RoleName);
                IdentityRole Found = await roleManager.FindByNameAsync(RoleName);
                
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return View();
                    }
                    else
                    {
                        TempData["errors"] = "";
                        foreach (var e in result.Errors)
                        {
                            TempData["errors"] += e.Description + "/n";
                        }
                        ViewData["RoleName"] = RoleName;
                        return View();
                    }
            }
            return View();
        }
    }
}
