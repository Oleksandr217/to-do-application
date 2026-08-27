using To_Do_Application_API.Models.Enums;

namespace To_Do_Application_API.Models.Domains
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateOnly? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public Guid? CategoryId { get; set; }

        public User User { get; set; }
        public Category? Category { get; set; }

        public TaskItem() { }
        public TaskItem(Guid id, string title, Guid userId, Guid? categoryId)
        {
            Id = id;
            Title = title;
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
