using JWTProject.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
