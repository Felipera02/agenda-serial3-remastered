using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Data;
using AgendaSerial3.Infrastructure.Data.Repository;

namespace AgendaSerial3.Application.Services;

public class CategoryService(CategoryRepository categoryRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<CategoryDto>> GetUserCategoriesAsync(string userId)
    {
        var categories = await _categoryRepository.FindAsync(c => c.UserId == userId);
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Color = c.Color
        });
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto, string userId)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
            Color = categoryDto.Color,
            UserId = userId
        };

        var createdCategory = await _categoryRepository.AddAsync(category);

        return new CategoryDto
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name,
            Color = createdCategory.Color
        };
    }

    public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto, string userId)
    {
        var category = await _categoryRepository
            .GetByExpression(c => c.Id == categoryDto.Id && c.UserId == userId);

        if (category == null)
            throw new UnauthorizedAccessException("Categoria não encontrada ou não pertence ao usuário");

        category.Name = categoryDto.Name;
        category.Color = categoryDto.Color;

        var updatedCategory = await _categoryRepository.UpdateAsync(category);

        return new CategoryDto
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name,
            Color = updatedCategory.Color
        };
    }

    public async Task DeleteCategoryAsync(int categoryId, string userId)
    {
        var category = await _categoryRepository
            .GetCategoryAndAppointmentsAsync(categoryId, userId);

        if (category == null)
            throw new UnauthorizedAccessException("Categoria não encontrada ou não pertence ao usuário");

        await _categoryRepository.DeleteAsync(category);
    }
}
