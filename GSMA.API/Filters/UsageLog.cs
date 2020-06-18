using GSMA.Infrastructure.Security;
using GSMA.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GSMA.API.Filters
{
    public class UsageLog : ActionFilterAttribute
    {
        public ILoggerManager logger;
        public UsageLog(ILoggerManager _logger)
        {
            logger = _logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress;
            var token = SecurityHelper.GetOAuthTokenFromHeader(context.HttpContext.Request.Headers);
            var email = SecurityHelper.GetClaim(token, "email");
            var controllerName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ActionName;
            if (string.IsNullOrEmpty(token))
            {
                logger.LogInfo("Start - Executing " + controllerName + " --> " + actionName);
            }
            else
            {
                logger.LogInfo("Start - Executing " + controllerName + " --> " + actionName + " for email: " + email);
            }

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress;
            var token = SecurityHelper.GetOAuthTokenFromHeader(context.HttpContext.Request.Headers);
            var email = SecurityHelper.GetClaim(token, "email");
            var controllerName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)context.Controller)
               .ControllerContext.ActionDescriptor.ActionName;
            if (string.IsNullOrEmpty(token))
            {
                logger.LogInfo("Start - Executing " + controllerName + " --> " + actionName);
            }
            else
            {
                logger.LogInfo("Start - Executing " + controllerName + " --> " + actionName + " for email: " + email);
            }
        }
    }
}
