namespace To_Do_Application_API.Models.DTOs.Tasks
{
    public class TaskQueryParams
    {
        public string? SearchTerm { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? IsCompleted { get; set; }

        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
