using Microsoft.AspNetCore.Mvc.Rendering;
using Project_MVC.Entity;

namespace Project_MVC.Models
{
    public class CreateModel
    {
        public User? User { get; set; }
        public DataVisits?DataVisits { get; set; }
        public IEnumerable<SelectListItem>? Category { get; set; }
        public IEnumerable<SelectListItem>? Locality { get; set; }
        public IEnumerable<SelectListItem>? Street { get; set; }
    }
}
