
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Magnise.Test.BL.DTO.API;

namespace Magnise.Test.API.Filters.ExceptionResolvers
{
    public class GlobalExceptionResolver : IExceptionResolver
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionResolver(
            ILogger<GlobalExceptionResolver> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _env = webHostEnvironment;
        }
        public void OnException(ExceptionContext context)
        {
            var id = Guid.NewGuid();
            var errorResponse = new ErrorResponse
            {
                Code = 500,
                ErrorMessage = context.Exception.Message
            };

            _logger.LogCritical(context.Exception, $"ErrorId : {id}");
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
