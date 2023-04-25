using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnet_api_first.Configurations.Filters
{
    public class MyFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This filter executed on :OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This filter executed on :OnActionExecuting");
        }
    }
}