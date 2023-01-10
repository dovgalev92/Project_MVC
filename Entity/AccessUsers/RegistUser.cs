using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity.AccessUsers
{
    public class RegistUser
    {
        [Required]
        [Remote(action: "LogInUser", controller: "Access", ErrorMessage = "Данный логин уже занят")]
        public string RegistName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password", ErrorMessage = "Обнаружено несовпадение пароля")]
        public string? RepeatPassword { get; set; }
    }
}
