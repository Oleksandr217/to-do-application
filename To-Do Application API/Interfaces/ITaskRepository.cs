using To_Do_Application_API.Models.Domains;
using To_Do_Application_API.Models.DTOs.Tasks;

namespace To_Do_Application_API.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<(List<TaskItem> Items, int TotalCount)> GetFilteredAsync(Guid userId, TaskQueryParams query);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
    }
}
