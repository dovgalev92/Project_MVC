using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_MVC.Context_DataBase;
using Project_MVC.Entity;
using Project_MVC.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Project_MVC.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly ContextDb context;
        public CreateModel model = new CreateModel();
        public SettingController(ContextDb context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Setting_Locality()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Setting_Locality(Locality locality)
        {
            if (ModelState.IsValid)
            {
                context.Localities.Add(locality);
                await context.SaveChangesAsync();
            }
            return View();
        }
        public async Task<IActionResult> Setting_Street()
        {
            ViewData["Locality"] = new SelectList(context.Set<Locality>(), "Id", "Title_Locality");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Setting_Street(Street street)
        {
            if (ModelState.IsValid)
            {
                context.Streets.Add(street);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Setting_Street));
        }
        public IActionResult Setting_Category()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Setting_Category(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }
            return View();
        }
       
    }

}
