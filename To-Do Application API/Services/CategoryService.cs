using To_Do_Application_API.Interfaces;
using To_Do_Application_API.Models.Domains;
using To_Do_Application_API.Models.DTOs.Categories;

namespace To_Do_Application_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponseDto>> GetAllAsync(Guid userId)
        {
            var categories = await _categoryRepository.GetAllByUserIdAsync(userId);

            var result = new List<CategoryResponseDto>();
            foreach (var category in categories)
            {
                int taskCount = await _categoryRepository.GetTaskCountAsync(category.Id);
                result.Add(MapToDto(category, taskCount));
            }

            return result;
        }

        public async Task<CategoryResponseDto> CreateAsync(Guid userId, CategoryCreateDto dto)
        {
            if (await _categoryRepository.ExistsByNameAsync(userId, dto.Name))
                throw new InvalidOperationException($"Категорія з назвою \"{dto.Name}\" вже існує");

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UserId = userId
            };

            await _categoryRepository.AddAsync(category);
            return MapToDto(category, 0);
        }

        public async Task<CategoryResponseDto> UpdateAsync(Guid categoryId, Guid userId, CategoryCreateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId)
                ?? throw new KeyNotFoundException("Категорію не знайдено");

            if (category.UserId != userId)
                throw new UnauthorizedAccessException("Ви не можете редагувати чужу категорію");

            if (await _categoryRepository.ExistsByNameAsync(userId, dto.Name) && category.Name != dto.Name)
                throw new InvalidOperationException($"Категорія з назвою \"{dto.Name}\" вже існує");

            category.Name = dto.Name;
            await _categoryRepository.UpdateAsync(category);

            int taskCount = await _categoryRepository.GetTaskCountAsync(category.Id);
            return MapToDto(category, taskCount);
        }

        public async Task DeleteAsync(Guid categoryId, Guid userId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId)
                ?? throw new KeyNotFoundException("Категорію не знайдено");

            if (category.UserId != userId)
                throw new UnauthorizedAccessException("Ви не можете видалити чужу категорію");

            await _categoryRepository.DeleteAsync(category);
        }

        private static CategoryResponseDto MapToDto(Category category, int taskCount)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                TaskCount = taskCount
            };
        }
    }
}
