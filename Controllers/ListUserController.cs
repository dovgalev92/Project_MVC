using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Create()
        {
            await GetData_DBContext(true);
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

        public IEnumerable<SelectListItem> GetStreet(int? ID)
        {
            if(ID != 0)
            {
                List<SelectListItem> selects_street = context.Streets.Where(s => s.LocalityId == ID).OrderBy(n => n.Title_Street)
                    .Select(n => new SelectListItem()
                    {
                        Value = n.Id.ToString(),
                        Text = n.Title_Street
                    }).ToList();
                 
                return selects_street;
            }
            return Enumerable.Empty<SelectListItem>();

        }
        public async Task GetData_DBContext(bool action)
        {
            if (action)
            {
                model.User = new User();
                List<SelectListItem> locality = await context.Localities.OrderBy(n => n.Title_Locality)
                    .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Locality }).ToListAsync();
                model.Locality = locality;

                List<SelectListItem> category = await context.Categories.OrderBy(n => n.Title_Category)
                    .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Category }).ToListAsync();
                model.Category = category;

                model.Street = new List<SelectListItem>();
            }
            else
            {
                List<SelectListItem> locality = await context.Localities.OrderBy(n => n.Title_Locality)
                   .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Locality }).ToListAsync();
                model.Locality = locality;

                List<SelectListItem> category = await context.Categories.OrderBy(n => n.Title_Category)
                    .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Category }).ToListAsync();
                model.Category = category;


                List<SelectListItem> street = await context.Streets.OrderBy(n => n.Title_Street)
                    .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Street }).ToListAsync();
                model.Street = street;
            }
        } // метод, который подтягивает данные с бд
        public async Task<IActionResult> Data_Update(int? id)
        {
            model.User = await context.Users.FirstOrDefaultAsync(u => u.User_Id == id);
            await GetData_DBContext(false);
            if(model.User!= null)
            {
                return View(model);
            }
            return View(nameof(List_User));
        }
        [HttpPost]
        public async Task<IActionResult> Data_Update(User user)
        {
            if (ModelState.IsValid)
            {
                context.Update(user);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(List_User));
            }
            return View(user);
        }
    }
}
