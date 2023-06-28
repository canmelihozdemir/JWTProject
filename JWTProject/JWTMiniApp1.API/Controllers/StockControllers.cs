﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTMiniApp1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockControllers : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStock()
        {
            var userName = HttpContext.User.Identity!.Name;

            var userIdClaim = User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier);

            return Ok($"Stock process=>     UserName: {userName} - UserId: {userIdClaim.Value}");
        }
    }
}
