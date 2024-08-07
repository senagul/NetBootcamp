using System.Net;

namespace NetBootcamp.API.Extensions
{
    public class IPWhiteListMiddleware(RequestDelegate next)
    {
        private readonly List<IPAddress> _whiteList = [ IPAddress.Parse("::1"), IPAddress.Parse("127.0.0.1") ];

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Path.Value.Contains("swagger"))
            {
                await next(context);
                return;

            }
            var ip = context.Connection.RemoteIpAddress;
          
            if(!_whiteList.Contains(ip))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("You are not authorized");
                return; 
            }
            await next(context);
        }
    }
}
