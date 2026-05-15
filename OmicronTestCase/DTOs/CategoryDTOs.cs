using System.ComponentModel.DataAnnotations;

namespace OmicronTestCase.DTOs;

public class CreateCategoryRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0, int.MaxValue)]
    public int MinStockQuantity { get; set; }
}

public class UpdateCategoryRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0, int.MaxValue)]
    public int MinStockQuantity { get; set; }
}

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinStockQuantity { get; set; }
}