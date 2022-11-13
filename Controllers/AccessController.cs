using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Controllers
{
    public class AccessController : Controller
    {

        public IActionResult LogInUser()
        {
            return View();
        }
       
    }
}
