using JWTProject.Core.DTOs;
using JWTProject.Core.Models;
using JWTProject.Core.Services;
using JWTProject.Service.Mapping;
using JWTProject.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JWTProject.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp { Email=createUserDto.Email,UserName=createUserDto.UserName};

            var result = await _userManager.CreateAsync(user,createUserDto.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x=>x.Description).ToList();

                return ResponseDto<UserAppDto>.Fail(new ErrorDto(errors,true),400);
            }

            return ResponseDto<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user),201);
        }

        public Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
