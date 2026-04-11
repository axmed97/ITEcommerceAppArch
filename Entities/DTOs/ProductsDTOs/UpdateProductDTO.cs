namespace Entities.DTOs.ProductsDTOs;

public record UpdateProductDTO(string Name,
    decimal Price,
    decimal Discount,
    int Count,
    Guid CategoryId,
    List<Guid> ColorIds,
    List<string> PhotoUrls);
