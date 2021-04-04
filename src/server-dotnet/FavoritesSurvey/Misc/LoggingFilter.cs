using FavoritesSurvey.Core;
using FavoritesSurvey.Core.Architecture.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesSurvey.Misc
{
    public class LoggingFilter : ActionFilterAttribute, IAsyncExceptionFilter
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var parameters = JsonConvert.SerializeObject(context.ActionArguments);
            var actionName = action.ActionName;
            var httpVerb = context.HttpContext.Request.Method;
            var requestPath = context.HttpContext.Request.Path;
            var correlationKey = Guid.NewGuid().ToString("N");

            var userIdentity = string.Empty;

            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            context.HttpContext.Items.Add("CorrelationKey", correlationKey);
            context.HttpContext.Items.Add("CurrentUser", userIdentity);

            var logVar = new LogVariables()
            {
                CorrelationKey = correlationKey,
                RequestPath = requestPath,
                HttpVerb = httpVerb,
                CurrentUser = userIdentity,
                Environment = config["Environment"],
            };

            Logger.Info($"{actionName} started.", logVar);

            if (actionName != "Authenticate")
                Logger.Info($"Parameters: {parameters}", logVar);

            return base.OnActionExecutionAsync(context, next);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var actionName = action.ActionName;
            var httpVerb = context.HttpContext.Request.Method;
            var requestPath = context.HttpContext.Request.Path;
            object correlationKey = "", user = "";

            context.HttpContext.Items.TryGetValue("CorrelationKey", out correlationKey);
            context.HttpContext.Items.TryGetValue("CurrentUser", out user);

            var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

            var logVar = new LogVariables()
            {
                CorrelationKey = correlationKey?.ToString() ?? string.Empty,
                RequestPath = requestPath,
                HttpVerb = httpVerb,
                CurrentUser = user?.ToString() ?? string.Empty,
                Environment = config["Environment"],
            };

            Logger.Info($"{actionName} ended.", logVar);

            return base.OnResultExecutionAsync(context, next);
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var httpVerb = context.HttpContext.Request.Method;
                var requestPath = context.HttpContext.Request.Path;
                object correlationKey = "", user = "";
                context.HttpContext.Items.TryGetValue("CorrelationKey", out correlationKey);
                context.HttpContext.Items.TryGetValue("CurrentUser", out user);

                var config = context.HttpContext.RequestServices.GetService<IConfiguration>();

                LogVariables logVar = new LogVariables()
                {
                    CorrelationKey = correlationKey?.ToString() ?? string.Empty,
                    RequestPath = requestPath,
                    HttpVerb = httpVerb,
                    CurrentUser = user?.ToString() ?? string.Empty,
                    Environment = config["Environment"],
                };

                Logger.Error(context.Exception, logVar);

                context.Result = new ObjectResult(ServiceResult.CreateError(context.Exception.Message));
            });
        }
    }
}
