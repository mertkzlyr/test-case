namespace OmicronTestCase.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int StockQuantity { get; set; }
    public bool IsLive { get; set; }
}