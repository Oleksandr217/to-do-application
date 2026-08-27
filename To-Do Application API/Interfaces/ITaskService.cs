using To_Do_Application_API.Models.DTOs.Common;
using To_Do_Application_API.Models.DTOs.Tasks;

namespace To_Do_Application_API.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResultDto<TaskResponseDto>> GetFilteredAsync(Guid userId, TaskQueryParams query);
        Task<TaskResponseDto> GetByIdAsync(Guid taskId, Guid userId);
        Task<TaskResponseDto> CreateAsync(Guid userId, TaskCreateDto dto);
        Task<TaskResponseDto> UpdateAsync(Guid taskId, Guid userId, TaskUpdateDto dto);
        Task DeleteAsync(Guid taskId, Guid userId);
    }
}
