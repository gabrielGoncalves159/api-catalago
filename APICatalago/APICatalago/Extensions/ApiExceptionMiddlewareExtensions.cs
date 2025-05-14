using APICatalago.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;

namespace APICatalago.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            //configurando o Middleware de exceção que sera executado quando um exceção não tratada for executada
            app.UseExceptionHandler(appError =>
            {
                //Esse código especifica o que fazer quando uma exceção não tratada for identificada, que será um tratamento de resposta para o
                //ambiente de desenvolvimento
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var conntextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (conntextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Message = conntextFeature.Error.Message,
                            Trace = conntextFeature.Error.StackTrace
                        }.ToString());
                    }
                });
            });
        }
    }
}
