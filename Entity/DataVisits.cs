using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity
{
    public class DataVisits
    {
        public int Id { get; set; }
        [DisplayName("Дата посещения")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DisplayName("Заметки")]
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
