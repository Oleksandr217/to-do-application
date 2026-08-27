using To_Do_Application_API.Models.DTOs.Categories;

namespace To_Do_Application_API.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync(Guid userId);
        Task<CategoryResponseDto> CreateAsync(Guid userId, CategoryCreateDto dto);
        Task<CategoryResponseDto> UpdateAsync(Guid categoryId, Guid userId, CategoryCreateDto dto);
        Task DeleteAsync(Guid categoryId, Guid userId);
    }
}
