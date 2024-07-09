using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace NetBootcamp.API.Filters
{
    public class MyResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var responseBody = (context.Result as ObjectResult).Value;
            
            if(responseBody is ResponseModelDto<ProductDto>)
            {


                //loglama
            }



                
            Console.WriteLine("OnResultExecuting");
        }
    }
}
