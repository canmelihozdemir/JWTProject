using JWTProject.Core.Configurations;
using JWTProject.Core.DTOs;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Core.Services;
using JWTProject.Core.UnitOfWorks;
using JWTProject.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }


        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return ResponseDto<TokenDto>.Fail("Email or Password wrong",400,true);

            if (!(await _userManager.CheckPasswordAsync(user,loginDto.Password))) return ResponseDto<TokenDto>.Fail("Email or Password wrong", 400, true);

            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenRepository.Where(x=>x.UserId==user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return ResponseDto<TokenDto>.Success(token,200);
        }

        public ResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x=>x.Id==clientLoginDto.ClientId && x.Secret==clientLoginDto.ClientSecret);

            if(client == null)  return ResponseDto<ClientTokenDto>.Fail("ClientId or ClientSecret not found",404,true);

            var token = _tokenService.CreateTokenByClient(client);

            return ResponseDto<ClientTokenDto>.Success(token,200);
        }

        public Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
