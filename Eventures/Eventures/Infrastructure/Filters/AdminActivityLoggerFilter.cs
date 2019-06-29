using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Eventures.Infrastructure.Filters
{
    public class AdminActivityLoggerFilter : IActionFilter
    {
        private readonly ILogger<AdminActivityLoggerFilter> logger;

        public AdminActivityLoggerFilter(ILogger<AdminActivityLoggerFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.logger.LogInformation(context.ActionDescriptor.DisplayName);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
