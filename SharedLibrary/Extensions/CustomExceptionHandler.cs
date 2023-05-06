using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Exceptions;
using System.Text.Json;

namespace SharedLibrary.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //Run middleware => sonlandirici midleware-dir.
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    //bu interface sahesinde xetaleri elde eliyecem =>IExceptionHandlerFeature();
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature != null)
                    {
                        //exception-i elde eliyirem.
                        var ex = errorFeature.Error;

                        ErrorDto errorDto = null;

                        if (ex is CustomException)
                            errorDto = new ErrorDto(ex.Message, true);
                        else
                            errorDto = new ErrorDto(ex.Message, false);

                        var response = Response<NoDataDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}