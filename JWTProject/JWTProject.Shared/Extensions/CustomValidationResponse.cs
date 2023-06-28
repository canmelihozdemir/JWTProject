using JWTProject.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Shared.Extensions
{
    public static class CustomValidationResponse
    {
        public static void AddCustomValidationReponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options=>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    var errorDto = new ErrorDto(errors.ToList(), true);

                    var response = ResponseDto<NoDataDto>.Fail(errorDto, 400);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
