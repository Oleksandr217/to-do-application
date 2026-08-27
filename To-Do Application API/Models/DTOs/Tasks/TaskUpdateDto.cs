using System.ComponentModel.DataAnnotations;
using To_Do_Application_API.Models.Enums;

namespace To_Do_Application_API.Models.DTOs.Tasks
{
    public class TaskUpdateDto
    {
        [Required(ErrorMessage = "Назва задачі є обов'язковою")]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public TaskPriority Priority { get; set; }

        public DateOnly? DueDate { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
