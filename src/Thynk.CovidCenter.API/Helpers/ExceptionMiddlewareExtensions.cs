using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using Thynk.CovidCenter.Core.Constants;
using Thynk.CovidCenter.Core.ResponseModel;

namespace Thynk.CovidCenter.API.Helpers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    IExceptionHandlerPathFeature contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (contextFeature is not null)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new BaseResponse
                        {
                            Message = ResponseMessages.GenericException
                        }));
                    }
                });
            });
        }
    }
}
