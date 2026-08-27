using Microsoft.EntityFrameworkCore;
using To_Do_Application_API.Interfaces;
using To_Do_Application_API.Models.Domains;

namespace To_Do_Application_API.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(Guid userId, string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.UserId == userId && c.Name.ToLower() == name.ToLower());
        }

        public async Task<int> GetTaskCountAsync(Guid categoryId)
        {
            return await _context.Tasks
                .CountAsync(t => t.CategoryId == categoryId);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
