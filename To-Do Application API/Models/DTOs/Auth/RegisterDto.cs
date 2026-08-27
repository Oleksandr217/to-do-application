using System.ComponentModel.DataAnnotations;

namespace To_Do_Application_API.Models.DTOs.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ім'я є обов'язковим")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email є обов'язковим")]
        [EmailAddress(ErrorMessage = "Некоректний формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        [MinLength(6, ErrorMessage = "Пароль має містити щонайменше 6 символів")]
        public string Password { get; set; }
    }
}
