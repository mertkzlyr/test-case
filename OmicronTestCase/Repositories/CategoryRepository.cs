using Microsoft.EntityFrameworkCore;
using OmicronTestCase.Data;
using OmicronTestCase.Models;

namespace OmicronTestCase.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await dbContext.Categories.FindAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync();
        return category;

    }

    public async Task DeleteAsync(Category category)
    {
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> HasProductsAsync(int categoryId)
    {
        return await dbContext.Products.AnyAsync(p => p.CategoryId == categoryId);
    }
}