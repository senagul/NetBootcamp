using Microsoft.AspNetCore.Mvc.Filters;

namespace NetBootcamp.API.Filters
{
    public class MyResourceFilter : Attribute,IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var actionName = context.RouteData.Values["action"];
            Console.WriteLine("OnResourceExecuting");
        }
    }
}
