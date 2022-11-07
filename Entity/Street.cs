using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity
{
    public class Street
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Улица")]
        public string? Title_Street { get; set; }
        public int LocalityId { get; set; }
        public Locality? Locality { get; set; }
        public List<User>? User { get; set; }
    }
}
