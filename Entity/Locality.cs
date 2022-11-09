using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity
{
    public class Locality
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Населенный пункт")]
        public string? Title_Locality { get; set; }
        public List<Street>? Streets { get; set; }
        public List<User>? User { get; set; }
    }
}
