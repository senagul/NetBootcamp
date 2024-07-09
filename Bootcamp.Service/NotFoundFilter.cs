using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Bootcamp.Service
{
    public class NotFoundFilter(IProductRepository2 productRepository) : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;
            var productIdFromAction = context.ActionArguments.Values.First()!;
            int productId = 0;

            if (actionName == "UpdateProductName" && productIdFromAction is ProductNameUpdateRequestDto productNameUpdateRequestDto)
            {
                //if (productIdFromAction is not ProductNameUpdateRequestDto productNameUpdateRequestDto)
                //{
                //    return;
                //}
                productId = productNameUpdateRequestDto.Id;
            }


            if (productId == 0 && !int.TryParse(productIdFromAction.ToString(), out productId)) // route constrait var controllerda
            {
                return;
                //var errorMessage = "Id değeri sayısal olmalıdır.";

                //var responseModel = ResponseModelDto<NoContent>.Fail(errorMessage);
                //context.Result = new NotFoundObjectResult(responseModel);
            }

            var hasProduct = productRepository.HasExist(productId).Result;// metot asenkron olmadığı için .Result dedik

            if (!hasProduct)
            {
                var errorMessage = $"There is no product wih id: {productId}";

                var responseModel = ResponseModelDto<NoContent>.Fail(errorMessage);
                context.Result = new NotFoundObjectResult(responseModel);
            }


            //if (actionName == "Delete")
            //{
            //    var productIdFromAction = (int)context.ActionArguments.Values.First()!;

            //    if(int.TryParse(productIdFromAction.ToString(),out int productId))
            //    {
            //        var hasProduct = productRepository.GetById(productId).Result;// metot asenkron olmadığı için .Result dedik

            //        if (hasProduct is null)
            //        {
            //            var errorMessage = $"There is no product wih id: {productId}";

            //            var responseModel = ResponseModelDto<NoContent>.Fail(errorMessage);
            //            context.Result = new NotFoundObjectResult(responseModel);
            //        }
            //    }

            //}
        }
    }
}
