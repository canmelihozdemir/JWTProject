using JWTProject.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.API.Controllers
{
    public class CustomBaseControllers : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(ResponseDto<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
