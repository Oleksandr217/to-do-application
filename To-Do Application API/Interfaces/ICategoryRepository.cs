using To_Do_Application_API.Models.Domains;

namespace To_Do_Application_API.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<List<Category>> GetAllByUserIdAsync(Guid userId);
        Task<bool> ExistsByNameAsync(Guid userId, string name);
        Task<int> GetTaskCountAsync(Guid categoryId);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
