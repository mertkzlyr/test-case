using Microsoft.EntityFrameworkCore;
using OmicronTestCase.Data;
using OmicronTestCase.DTOs;
using OmicronTestCase.Models;

namespace OmicronTestCase.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<List<Product>> GetAllAsync()
    {
        return await dbContext.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> FilterAsync(ProductFilterRequest filter)
    {
        var query = dbContext.Products.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var kw = filter.Keyword.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(kw) ||
                (p.Description != null && p.Description.ToLower().Contains(kw)) ||
                p.Category.Name.ToLower().Contains(kw));
        }

        if (filter.MinStock.HasValue)
            query = query.Where(p => p.StockQuantity >= filter.MinStock.Value);

        if (filter.MaxStock.HasValue)
            query = query.Where(p => p.StockQuantity <= filter.MaxStock.Value);

        return await query.ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(Product product)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }
}