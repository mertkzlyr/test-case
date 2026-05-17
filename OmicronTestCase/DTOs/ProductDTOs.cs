using System.ComponentModel.DataAnnotations;

namespace OmicronTestCase.DTOs;

public class CreateProductRequest
{
    [Required][MinLength(1)][MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}

public class UpdateProductRequest
{
    [Required][MinLength(1)][MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}

public class ProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public bool IsLive { get; set; }
}

public class ProductFilterRequest
{
    public string? Keyword { get; set; }
    public int? MinStock { get; set; }
    public int? MaxStock { get; set; }
}