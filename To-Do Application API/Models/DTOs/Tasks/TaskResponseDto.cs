using To_Do_Application_API.Models.Enums;

namespace To_Do_Application_API.Models.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public TaskPriority Priority { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
