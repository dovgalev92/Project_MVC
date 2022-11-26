using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity.AccessUsers
{
    public class LogInUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string? Password { get; set; }
    }
}
