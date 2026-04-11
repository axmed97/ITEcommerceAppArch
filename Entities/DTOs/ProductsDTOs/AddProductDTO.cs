namespace Entities.DTOs.ProductsDTOs;

public class AddProductDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Count { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> ColorIds { get; set; }
    public List<string> PhotoUrls { get; set; }
}
