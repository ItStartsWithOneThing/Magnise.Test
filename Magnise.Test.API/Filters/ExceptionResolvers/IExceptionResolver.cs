using Microsoft.AspNetCore.Mvc.Filters;

namespace Magnise.Test.API.Filters.ExceptionResolvers
{
    public interface IExceptionResolver
    {
        public void OnException(ExceptionContext context);
    }
}
