using System.ComponentModel;

namespace Project_MVC.Entity
{
    public class Hause_Details
    {
        public int Id { get; set; }
        [DisplayName("Система отопления")]
        public string? Heating_System { get; set; }
        [DisplayName("Пожарный извещатель")]
        public string? Fire_Detector { get; set; }
        [DisplayName("Заметки")]
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public List<User>? User { get; set; }

    }
}
