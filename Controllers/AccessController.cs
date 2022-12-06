using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC.Context_DataBase;
using Project_MVC.Entity.AccessUsers;
using Project_MVC.Models;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Project_MVC.Controllers
{
    
    public class AccessController : Controller
    {
        private readonly ContextDb context;
        AccessModel model = new AccessModel();
        public AccessController(ContextDb context)
        {
            this.context = context;
        }

        public IActionResult LogInUser()
        {
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogInUser(LogInUser user)
        {
            if (ModelState.IsValid)
            {
                using (SHA256 sHA256 = SHA256.Create()) {

                    var bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    var getbytes = BitConverter.ToString(bytes).Replace("-", "").ToUpper();

                    var person = context.LogInUsers.FirstOrDefault(p => p.Password.Equals(getbytes));

                    if (person != null)
                    {
                        await AuthenticationUser(user.Login);
                        return RedirectToAction("List_User", "ListUser");
                    }
                };

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RegistrUser(RegistUser user)
        {
            if (ModelState.IsValid)
            {
                var person = await context.LogInUsers.Where(u => u.Login.Equals(user.Login)).FirstOrDefaultAsync();
                if(person == null)
                {
                    using (SHA256 sha = SHA256.Create())
                    {
                        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                        var sha256 = BitConverter.ToString(bytes).Replace("-", "").ToUpper();

                        context.LogInUsers.Add(new LogInUser
                        {
                            Login = user.Login,
                            Password = sha256
                        });
                    }
                    await context.SaveChangesAsync();

                    await AuthenticationUser(user.Login);
                    return RedirectToAction("List_User", "ListUser");
                }
            }
            return View(user);
        }


        public async Task AuthenticationUser(string login)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claim, "Cookies", ClaimsIdentity.DefaultNameClaimType, 
                                          ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity));

        }
        public async Task<IActionResult> ExitProgramm()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("LogInUser", "Access");
        }
    }
}
