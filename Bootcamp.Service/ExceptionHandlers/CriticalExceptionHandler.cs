using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bootcamp.Service.ExceptionHandlers
{
    public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if (exception is CriticalExceptionHandler)
            {
                logger.LogInformation($"hata mesajı gönderildi(sms). {exception.Message}");
            }
            return ValueTask.FromResult(false); // Global Exception Handlerın hatayı ele alması için burada false dönüyoruz
        }
    }
}
