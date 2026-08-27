using To_Do_Application_API.Interfaces;
using To_Do_Application_API.Models.Domains;
using To_Do_Application_API.Models.DTOs.Common;
using To_Do_Application_API.Models.DTOs.Tasks;

namespace To_Do_Application_API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<PagedResultDto<TaskResponseDto>> GetFilteredAsync(Guid userId, TaskQueryParams query)
        {
            var (items, totalCount) = await _taskRepository.GetFilteredAsync(userId, query);

            return new PagedResultDto<TaskResponseDto>
            {
                Items = items.Select(MapToDto).ToList(),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<TaskResponseDto> GetByIdAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId)
                ?? throw new KeyNotFoundException("Задачу не знайдено");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException("Ви не можете переглядати чужу задачу");

            return MapToDto(task);
        }

        public async Task<TaskResponseDto> CreateAsync(Guid userId, TaskCreateDto dto)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                CategoryId = dto.CategoryId,
                UserId = userId,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);

            var created = await _taskRepository.GetByIdAsync(task.Id);
            return MapToDto(created!);
        }

        public async Task<TaskResponseDto> UpdateAsync(Guid taskId, Guid userId, TaskUpdateDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(taskId)
                ?? throw new KeyNotFoundException("Задачу не знайдено");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException("Ви не можете редагувати чужу задачу");

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.CategoryId = dto.CategoryId;

            await _taskRepository.UpdateAsync(task);

            var updated = await _taskRepository.GetByIdAsync(task.Id);
            return MapToDto(updated!);
        }

        public async Task DeleteAsync(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId)
                ?? throw new KeyNotFoundException("Задачу не знайдено");

            if (task.UserId != userId)
                throw new UnauthorizedAccessException("Ви не можете видалити чужу задачу");

            await _taskRepository.DeleteAsync(task);
        }

        private static TaskResponseDto MapToDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name
            };
        }
    }
}
