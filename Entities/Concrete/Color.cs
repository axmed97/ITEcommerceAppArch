using Entities.Concrete.Common;

namespace Entities.Concrete;

public class Color : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public List<ProductColor> ProductColors { get; set; }

}
