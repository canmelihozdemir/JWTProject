using JWTProject.Core.DTOs;
using JWTProject.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<ResponseDto<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken);
        Task<ResponseDto<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientLoginDto);
    }
}
