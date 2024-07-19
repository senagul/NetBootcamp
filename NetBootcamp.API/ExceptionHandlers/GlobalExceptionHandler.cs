using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace NetBootcamp.API.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var responseModel = ResponseModelDto<NoContent>.Fail(exception.Message, HttpStatusCode.InternalServerError);

            await httpContext.Response.WriteAsJsonAsync(responseModel,cancellationToken);
            return true;
        }
    }
}
