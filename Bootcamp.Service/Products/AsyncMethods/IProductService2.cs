using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.SharedDTOs;
using System.Collections.Immutable;

namespace Bootcamp.Service.Products.AsyncMethods
{
    public interface IProductService2
    {
        Task<ResponseModelDto<ImmutableList<ProductDto>>> GetAllWithCalculatedTax(PriceCalculator priceCalculator);
        Task<ResponseModelDto<ProductDto?>> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator);
        Task<ResponseModelDto<ImmutableList<ProductDto>>> GetAllByPageWithCalculatedTax(PriceCalculator priceCalculator, int page, int pageSize);
        Task<ResponseModelDto<int>> Create(ProductCreateRequestDto request);
        Task<ResponseModelDto<NoContent>> Update(int productId, ProductUpdateRequestDto request);
        Task<ResponseModelDto<NoContent>> UpdateProductName(int id, string name);
        Task<ResponseModelDto<NoContent>> Delete(int id);
    }
}