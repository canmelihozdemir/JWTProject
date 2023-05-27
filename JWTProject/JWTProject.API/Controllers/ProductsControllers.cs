using JWTProject.Core.DTOs;
using JWTProject.Core.Models;
using JWTProject.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsControllers : CustomBaseControllers
    {
        private readonly IGenericService<Product,ProductDto> _service;
        
        public ProductsControllers(IGenericService<Product, ProductDto> service)
        {
            _service = service;
        }

        [HttpGet] public async Task<IActionResult> GetAll() => ActionResultInstance(await _service.GetAllAsync());

        [HttpPost] public async Task<IActionResult> Save(ProductDto productDto) => ActionResultInstance(await _service.AddAsync(productDto));

        [HttpPut] public async Task<IActionResult> Update(ProductDto productDto) => ActionResultInstance(await _service.Update(productDto, productDto.Id));

        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) => ActionResultInstance(await _service.Remove(id));



    }
}
