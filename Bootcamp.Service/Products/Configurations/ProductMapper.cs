using AutoMapper;
using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;


namespace Bootcamp.Service.Products.Configurations
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>().ForPath(x => x.Created, y => y.MapFrom(y => y.Created.ToShortDateString())).
                                             ForPath(x => x.Price, y => y.MapFrom(y => new PriceCalculator().CalculateKdv(y.Price, 1.20m))).ReverseMap();
        }
    }
}
