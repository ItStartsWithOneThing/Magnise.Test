
using Magnise.Test.API.Filters.ExceptionResolvers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Magnise.Test.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var globalResolver = context.HttpContext.RequestServices.GetService<IExceptionResolver>();
            globalResolver.OnException(context);
        }
    }
}
