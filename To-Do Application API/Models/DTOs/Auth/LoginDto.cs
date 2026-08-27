using System.ComponentModel.DataAnnotations;

namespace To_Do_Application_API.Models.DTOs.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email є обов'язковим")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        public string Password { get; set; }
    }
}
