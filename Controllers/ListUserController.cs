using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MVC.Context_DataBase;
using Project_MVC.Entity;
using Project_MVC.Models;

namespace Project_MVC.Controllers
{
    [Authorize]
    public class ListUserController : Controller
    {
        private readonly ContextDb context;
        public CreateModel model = new CreateModel();

        public ListUserController(ContextDb _context)
        {
            context = _context;
        }


        public async Task<IActionResult> List_User(string? search, int page = 0)
        {
            var result_searchUser = from user in context.Users select user;
            if (!string.IsNullOrWhiteSpace(search))
            {
                result_searchUser = context.Users.Where(u => u.Name.Contains(search) || u.Surname.Contains(search) || u.Patronymic.Contains(search))
                    .Include(c => c.Category).Include(l => l.Locality).Include(s => s.Street).AsNoTracking();
                return View(await result_searchUser.ToListAsync());
            }
            else
            {
                const int pageSize = 8;
                var result_list = await context.Users.Include(p => p.Category).Include(l => l.Locality).Include(s => s.Street).ToListAsync();
                var count = result_list.Count();
                var data = result_list.Skip(page * pageSize).Take(pageSize).ToList();
                this.ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
                this.ViewBag.Page = page;
                return View(data);
            }
        }
        public async Task<IActionResult> Create()
        {
            await GetData_DBContext();
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
            if (ID != 0)
            {
                List<SelectListItem> selects_street = context.Streets.Where(s => s.LocalityId.Equals(ID)).OrderBy(n => n.Title_Street)
                    .Select(n => new SelectListItem()
                    {
                        Value = n.Id.ToString(),
                        Text = n.Title_Street
                    }).AsNoTracking().ToList();

                return selects_street;
            }
            return Enumerable.Empty<SelectListItem>();

        }
        public async Task<IActionResult> Data_Update(int? id)
        {
            model.User = await context.Users.FirstOrDefaultAsync(u => u.User_Id == id);
            await GetData_of_UpadateUser();
            if (model.User != null)
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
        public async Task<IActionResult> Delete_User(int? id)
        {
            var user_del = await context.Users.FindAsync(id);
            context.Users.Remove(user_del);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(List_User));
        }

        public IActionResult Viewing_Date(int? id)
        {
            var list_date = from date in context.DataVisits.Where(i => i.UserId == id) select date;
            return View(list_date);
        }
        public async Task<IActionResult> Add_DateOfVisits(int? id)
        {
            model.User = await context.Users.FindAsync(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add_DateOfVisits([Bind("Date,Notes,UserId")] DataVisits data)
        {

            if (ModelState.IsValid)
            {
                context.DataVisits.Add(data);
                await context.SaveChangesAsync();
                return RedirectToAction("List_User");
            }
            else
            {
                return RedirectToAction(nameof(Add_DateOfVisits));
            }

        }
        public async Task GetData_of_UpadateUser()
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
        }// метод возвращает данные с бд при обновлении гражданина в методе Data_Update
        public async Task GetData_DBContext()
        {
            model.User = new User();
            List<SelectListItem> locality = await context.Localities.OrderBy(n => n.Title_Locality)
                .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Locality }).ToListAsync();
            model.Locality = locality;

            List<SelectListItem> category = await context.Categories.OrderBy(n => n.Title_Category)
                .Select(n => new SelectListItem() { Value = n.Id.ToString(), Text = n.Title_Category }).ToListAsync();
            model.Category = category;

            model.Street = new List<SelectListItem>();

        } // метод возвращающий данные с бд(список населенных пунктов, список категорий)

    }
}
