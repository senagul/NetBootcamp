using System.Collections.Immutable;
using System.Net;
using AutoMapper;
using Bootcamp.Repository;
using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Mvc;


namespace Bootcamp.Service.Products.AsyncMethods
{
    public class ProductService2(IProductRepository2 productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService2
    {
        public async Task<ResponseModelDto<int>> Create(ProductCreateRequestDto request)
        {
            var newProduct = new Product
            {
                //Id = productRepository.GetAll().Count + 1,
                Name = request.Name.Trim(),
                Price = request.Price,
                Stock = 10,
                Barcode = Guid.NewGuid().ToString(),
                Created = DateTime.Now
            };

            await productRepository.Create(newProduct);
            await unitOfWork.CommitAsync();
            return ResponseModelDto<int>.Success(newProduct.Id, HttpStatusCode.Created);
        }

        public async Task<ResponseModelDto<NoContent>> Delete(int id)
        {
            await productRepository.Delete(id);
            await unitOfWork.CommitAsync();

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }

        public async Task<ResponseModelDto<ImmutableList<ProductDto>>> GetAllByPageWithCalculatedTax(PriceCalculator priceCalculator, int page, int pageSize)
        {
            var productsList = await productRepository.GetAllByPage(page, pageSize);
            var productListAsDto = mapper.Map<List<Product>, List<ProductDto>>(productsList.ToList());
            //var productListAsDto = productsList.Select(product => new ProductDto(
            //    product.Id,
            //    product.Name,
            //    priceCalculator.CalculateKdv(product.Price, 1.20m),
            //    product.Created.ToShortDateString()
            //)).ToImmutableList();


            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productListAsDto.ToImmutableList());
        }

        public async Task<ResponseModelDto<ImmutableList<ProductDto>>> GetAllWithCalculatedTax(PriceCalculator priceCalculator)
        {
            var productList = await productRepository.GetAll();
            //var productList = ().Select(product => new ProductDto(
            //    product.Id,
            //    product.Name,
            //    priceCalculator.CalculateKdv(product.Price, 1.20m),
            //    product.Created.ToShortDateString()
            //)).ToImmutableList();

            var productListAsDto = mapper.Map<List<Product>, List<ProductDto>>(productList.ToList());
            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productListAsDto.ToImmutableList());
        }

        public async Task<ResponseModelDto<ProductDto?>> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator)
        {
            var hasProduct = await productRepository.GetById(id);

            //if (hasProduct is null)
            //{
            //    return ResponseModelDto<ProductDto?>.Fail("Ürün bulunamadı", HttpStatusCode.NotFound);
            //}

            var productAsDto = mapper.Map<ProductDto>(hasProduct);

            //var productAsDto = new ProductDto(
            //    hasProduct.Id,
            //    hasProduct.Name,
            //    priceCalculator.CalculateKdv(hasProduct.Price, 1.20m),
            //    hasProduct.Created.ToShortDateString()
            //);

            return ResponseModelDto<ProductDto?>.Success(productAsDto);
        }

        public async Task<ResponseModelDto<NoContent>> Update(int productId, ProductUpdateRequestDto request)
        {
            var hasProduct = await productRepository.GetById(productId);

            //if (hasProduct is null)
            //{
            //    return ResponseModelDto<NoContent>.Fail("Güncellenmeye çalışılan ürün bulunamadı.",
            //        HttpStatusCode.NotFound);
            //}


            hasProduct.Name = request.Name;
            hasProduct.Price = request.Price;


            await productRepository.Update(hasProduct);
            await unitOfWork.CommitAsync();

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }

        public async Task<ResponseModelDto<NoContent>> UpdateProductName(int id, string name)
        {
            await productRepository.UpdateProductName(name, id);
            await unitOfWork.CommitAsync();

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);

        }
    }
}