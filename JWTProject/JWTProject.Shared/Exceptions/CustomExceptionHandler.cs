using JWTProject.Shared.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWTProject.Shared.Exceptions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config=>
            {
                //run-end
                config.Run(async context=>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorFeature=context.Features.Get<IExceptionHandlerFeature>();

                    ErrorDto errorDto = null;


                    if (errorFeature!=null)
                    {
                        var ex = errorFeature.Error;

                        if(ex is CustomException)
                        {
                            errorDto = new ErrorDto(ex.Message,true);
                        }
                        else
                        {
                            errorDto = new ErrorDto(ex.Message, false);
                        }


                        var response = ResponseDto<NoDataDto>.Fail(errorDto,500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                    }


                });
            });
        }
    }
}
