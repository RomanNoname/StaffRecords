using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace StaffRecords.Host.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorMessage = $"INTERNAL SERVER ERROR {contextFeature.Error} {context.Response.StatusCode} ";
                        logger.LogError(errorMessage);
}
                });
            });
        }
    }
}
