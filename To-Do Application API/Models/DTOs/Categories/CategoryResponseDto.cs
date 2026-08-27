namespace To_Do_Application_API.Models.DTOs.Categories
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TaskCount { get; set; }
    }
}
