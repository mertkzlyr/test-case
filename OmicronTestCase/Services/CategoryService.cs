using OmicronTestCase.DTOs;
using OmicronTestCase.Models;
using OmicronTestCase.Repositories;

namespace OmicronTestCase.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(MapToResponse).ToList();
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        return category is null ? null : MapToResponse(category);
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            MinStockQuantity = request.MinStockQuantity
        };
        var created = await categoryRepository.CreateAsync(category);
        return MapToResponse(created);
    }

    public async Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return null;

        category.Name = request.Name;
        category.MinStockQuantity = request.MinStockQuantity;

        var updated = await categoryRepository.UpdateAsync(category);
        return MapToResponse(updated);
    }

    public async Task<(bool success, string? error)> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return (false, "not_found");

        var hasProducts = await categoryRepository.HasProductsAsync(id);
        if (hasProducts) return (false, "has_products");

        await categoryRepository.DeleteAsync(category);
        return (true, null);
    }
    
    private static CategoryResponse MapToResponse(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        MinStockQuantity = c.MinStockQuantity
    };
}