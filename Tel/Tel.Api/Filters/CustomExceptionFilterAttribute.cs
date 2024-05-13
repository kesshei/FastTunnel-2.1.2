using TelServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Tel.Api.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "【全局异常捕获】");
            var res = new ApiResponse()
            {
                errorCode = ErrorCodeEnum.Exception,
                data = null,
                errorMessage = context.Exception.Message,
            };

            var result = new JsonResult(res) { StatusCode = 200 };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
