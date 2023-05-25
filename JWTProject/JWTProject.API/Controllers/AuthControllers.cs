using JWTProject.Core.DTOs;
using JWTProject.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthControllers : CustomBaseControllers
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthControllers(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost] public async Task<IActionResult> CreateTokenAsync(LoginDto loginDto) => ActionResultInstance(await _authenticationService.CreateTokenAsync(loginDto));

        [HttpPost] public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto) => ActionResultInstance(_authenticationService.CreateTokenByClient(clientLoginDto));

        [HttpPost] public async Task<IActionResult> RevokeRefreshTokenAsync(RefreshTokenDto refreshTokenDto) => ActionResultInstance(await _authenticationService.RevokeRefreshTokenAsync(refreshTokenDto.Token!));

        [HttpPost] public async Task<IActionResult> CreateTokenByRefreshTokenAsync(RefreshTokenDto refreshTokenDto) => ActionResultInstance(await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token!));

    }
}
