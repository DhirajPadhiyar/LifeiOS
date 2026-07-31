using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LifeiOS.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/html";

                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature != null)
                    {
                        context.Items["ErrorMessage"] = feature.Error.Message;
                    }

                    context.Request.Path = "/Home/Error";

                    var routeData = new RouteData();
                    routeData.Values["controller"] = "Home";
                    routeData.Values["action"] = "Error";

                    var actionContext = new ActionContext(
                        context,
                        routeData,
                        new ActionDescriptor());

                    var executor = context.RequestServices
                        .GetRequiredService<IActionResultExecutor<ViewResult>>();

                    var result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml"
                    };

                    await executor.ExecuteAsync(actionContext, result);
                });
            });
        }
    }
}