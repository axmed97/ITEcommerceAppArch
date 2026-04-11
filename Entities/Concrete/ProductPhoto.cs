using Entities.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete;

public class ProductPhoto : BaseEntity
{
    public string PhotoUrl { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
