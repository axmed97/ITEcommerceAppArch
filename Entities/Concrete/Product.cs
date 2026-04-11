using Entities.Concrete.Common;

namespace Entities.Concrete;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Count { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public List<ProductColor> ProductColors { get; set; }
    public List<ProductPhoto> ProductPhotos { get; set; }

}
