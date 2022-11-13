using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity.AccessUsers
{
    public class RegistUser : LogInUser
    {
        [Required]
        [Compare("Password", ErrorMessage = "Обнаружено несовпадение пароля")]
        public string? RepeatPassword { get; set; }
    }
}
