using Entities.Concrete.Common;

namespace Entities.Concrete;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
