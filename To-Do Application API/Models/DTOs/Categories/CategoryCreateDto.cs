using System.ComponentModel.DataAnnotations;

namespace To_Do_Application_API.Models.DTOs.Categories
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Назва категорії є обов'язковою")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
