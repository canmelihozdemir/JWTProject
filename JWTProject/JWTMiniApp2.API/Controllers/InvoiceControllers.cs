﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTMiniApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceControllers : ControllerBase
    {
        public IActionResult GetInvoices()
        {
            var userName = HttpContext.User.Identity!.Name;

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return Ok($"Invoice process=>     UserName: {userName} - UserId: {userId}");
        }
    }
}