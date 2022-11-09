using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_MVC.Context_DataBase;
using Project_MVC.Entity;
using Project_MVC.Models;

namespace Project_MVC.Controllers
{
    public class ListUserController : Controller
    {
        private readonly ContextDb context;
        public CreateModel model = new CreateModel();

        public ListUserController(ContextDb _context)
        {
            context = _context;
        }

        public async Task<IActionResult> List_User(string search)
        {
            var result_search = from user in context.Users select user;
            if (!string.IsNullOrWhiteSpace(search))
            {
                result_search = context.Users.Where(u => u.Name.Contains(search) || u.Surname.Contains(search) || u.Patronymic.Contains(search))
                    .Include(c => c.Category).Include(l => l.Locality).Include(s => s.Street);
                return View(await result_search.ToListAsync());
            }
            return View(await context.Users.Include(c => c.Category).Include(l => l.Locality).Include(s => s.Street).ToListAsync());
        }
        public IActionResult Create()
        {
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(List_User));
            }
            return View(model);
        }
    }
}
