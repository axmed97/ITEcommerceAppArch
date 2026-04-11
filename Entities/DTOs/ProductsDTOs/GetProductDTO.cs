using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.ProductsDTOs;

public class GetProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Count { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<string> PhotoUrls { get; set; }
    public List<GetProductColorDTO> Colors { get; set; }
}

public class GetProductColorDTO
{
    public Guid ColorId { get; set; }
    public string ColorCode { get; set; }
    public string ColorName { get; set; }
}