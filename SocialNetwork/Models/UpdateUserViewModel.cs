using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите имя")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Отчество", Prompt = "Введите Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательно для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "День рождения")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Аватар")]
        public string? Image { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Статус")]
        public string? Status { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "О вас")]
        public string? About { get; set; }

        public string UserName => Email;
    }
}
