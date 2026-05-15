using OmicronTestCase.DTOs;

namespace OmicronTestCase.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetAllAsync();
    Task<ProductResponse?> GetByIdAsync(int id);
    Task<List<ProductResponse>> FilterAsync(ProductFilterRequest filter);
    Task<(ProductResponse? product, string? error)> CreateAsync(CreateProductRequest request);
    Task<(ProductResponse? product, string? error)> UpdateAsync(int id, UpdateProductRequest request);
    Task<bool> DeleteAsync(int id);
}