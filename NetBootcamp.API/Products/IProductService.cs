﻿using NetBootcamp.API.DTOs;
using NetBootcamp.API.Products.DTOs;
using System.Collections.Immutable;

namespace NetBootcamp.API.Products
{
    public interface IProductService
    {
        ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax(PriceCalculator priceCalculator);
        ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator);
        ResponseModelDto<int> Create(ProductCreateRequestDto request);
        ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request);

        ResponseModelDto<NoContent> Delete(int id);
    }
}