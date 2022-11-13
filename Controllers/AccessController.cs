using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC.Context_DataBase;
using Project_MVC.Entity.AccessUsers;
using Project_MVC.Models;
using System.Security.Claims;

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
                var person = await context.LogInUsers.FirstOrDefaultAsync(u => u.Login.Equals(user.Login)
                             && u.Password.Equals(user.Password));

                if (person!= null)
                {
                    await AuthenticationUser(user.Login);
                    return RedirectToAction("List_User", "ListUser");
                }
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
                    context.LogInUsers.Add(new LogInUser
                    {
                        Login = user.Login,
                        Password = user.Password
                    });
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

    }
}
