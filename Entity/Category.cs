using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Категория")]
        public string? Title_Category { get; set; }
        public List<User> User { get; set; }
    }
}
