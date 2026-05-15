namespace OmicronTestCase.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinStockQuantity { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}