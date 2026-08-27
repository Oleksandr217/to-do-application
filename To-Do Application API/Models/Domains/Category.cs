namespace To_Do_Application_API.Models.Domains
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public Category() { }
        public Category(Guid id, string name, Guid userId)
        {
            Id = id;
            Name = name;
            UserId = userId;
        }
    }
}
