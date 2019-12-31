using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Core.Main.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        public WebApiExceptionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError("---------------异常信息---------------");
            _logger.LogError(context.Exception.Message);
            _logger.LogError("---------------异常堆栈---------------");
            _logger.LogError(context.Exception.StackTrace);
            _logger.LogError("--------------------------------------");
            base.OnException(context);
        }
    }
}
