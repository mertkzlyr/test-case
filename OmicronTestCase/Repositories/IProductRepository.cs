using OmicronTestCase.DTOs;
using OmicronTestCase.Models;

namespace OmicronTestCase.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> FilterAsync(ProductFilterRequest filter);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task UpdateIsLiveForCategoryAsync(int categoryId, int minStockQuantity);
}