using Microsoft.EntityFrameworkCore;
using To_Do_Application_API.Interfaces;
using To_Do_Application_API.Models.Domains;
using To_Do_Application_API.Models.DTOs.Tasks;

namespace To_Do_Application_API.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<(List<TaskItem> Items, int TotalCount)> GetFilteredAsync(Guid userId, TaskQueryParams query)
        {
            var tasksQuery = _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.UserId == userId);

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var search = query.SearchTerm.ToLower();
                tasksQuery = tasksQuery.Where(t =>
                    t.Title.ToLower().Contains(search) ||
                    (t.Description != null && t.Description.ToLower().Contains(search)));
            }

            if (query.CategoryId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.CategoryId == query.CategoryId.Value);
            }

            if (query.IsCompleted.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.IsCompleted == query.IsCompleted.Value);
            }

            int totalCount = await tasksQuery.CountAsync();

            var items = await tasksQuery
                .OrderByDescending(t => t.CreatedAt)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
