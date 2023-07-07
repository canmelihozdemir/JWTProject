using JWTProject.Core.DTOs;
using JWTProject.Core.Models;
using JWTProject.Core.Services;
using JWTProject.Service.Mapping;
using JWTProject.Shared.DTOs;
using JWTProject.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            //throw new CustomException("Random Error for Test");

            var user = new UserApp { Email=createUserDto.Email,UserName=createUserDto.UserName};

            var result = await _userManager.CreateAsync(user,createUserDto.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x=>x.Description).ToList();

                return ResponseDto<UserAppDto>.Fail(new ErrorDto(errors,true),400);
            }

            return ResponseDto<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user),201);
        }

        public async Task<ResponseDto<NoDataDto>> CreateUserRolesAsync(string userName,string roleName)
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name="admin"});
                await _roleManager.CreateAsync(new() { Name="manager"});
                
            }

            var user=await _userManager.FindByNameAsync(userName);

            if (user == null) return ResponseDto<NoDataDto>.Fail("Username not found", 404, true);

            await _userManager.AddToRoleAsync(user, roleName);

            return ResponseDto<NoDataDto>.Success(StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return ResponseDto<UserAppDto>.Fail("Username not found", 404, true);

            return ResponseDto<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 201);
        }
    }
}
