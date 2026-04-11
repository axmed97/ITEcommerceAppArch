using Entities.Concrete.Common;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete;

public class Order : BaseEntity
{
    public Guid TrackId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}


