using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.ExceptionHandlers;
using System.Diagnostics;

namespace NetBootcamp.API.Extensions
{
    public static class ServiceExt
    {
        public static void AddService(this IServiceCollection services)
        {


            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddProductService();

            services.AddExceptionHandler<CriticalExceptionHandler>();   
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
