using JWTProject.Core.DTOs;
using JWTProject.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : CustomBaseControllers
    {
        private readonly IUserService _userService;

        public UserControllers(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost] public async Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto) => ActionResultInstance(await _userService.CreateUserAsync(createUserDto));

        [Authorize][HttpGet] public async Task<IActionResult> GetUserAsync() => ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity!.Name!));

        [HttpPost("CreateUserRolesAsync")] public async Task<IActionResult> CreateUserRolesAsync(string userName,string roleName) => ActionResultInstance(await _userService.CreateUserRolesAsync(userName,roleName));

    }
}
