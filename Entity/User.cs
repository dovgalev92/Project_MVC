using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Entity
{
    public class User
    {
        [Key]
        [Required]
        [DisplayName("№")]
        public int User_Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }
        [Required]
        [DisplayName("Дата рождения")]
        public DateTime Birth_Day { get; set; }
        [Required]
        [DisplayName("Номер дома")]
        public string? Number_Haus { get; set; }
        [DisplayName("Статус посещения")]
        public string? Status_Visits { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int LocalityId { get; set; }
        public Locality? Locality { get; set; }
        public int StreetId { get; set; }
        public Street? Street { get; set; }
        public DataVisits? DataVisits { get; set; }
        public Hause_Details? HauseDetails { get; set; }

    }
}
