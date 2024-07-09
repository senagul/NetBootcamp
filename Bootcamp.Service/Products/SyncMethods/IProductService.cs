
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.SharedDTOs;
using System.Collections.Immutable;

namespace Bootcamp.Service.Products.SyncMethods
{
    public interface IProductService
    {
        ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax(PriceCalculator priceCalculator);
        ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator);
        ResponseModelDto<ImmutableList<ProductDto>> GetAllByPageWithCalculatedTax(PriceCalculator priceCalculator, int page, int pageSize);
        ResponseModelDto<int> Create(ProductCreateRequestDto request);
        ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request);
        ResponseModelDto<NoContent> UpdateProductName(int id, string name);
        ResponseModelDto<NoContent> Delete(int id);
    }
}