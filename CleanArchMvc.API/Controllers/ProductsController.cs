using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _service.GetProducts();
            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _service.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto product)
        {
            if (product == null)
                return BadRequest("Invalid data format");

            await _service.Add(product);
            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto product)
        {
            if (id != product.Id)
                return BadRequest();

            if (product == null)
                return BadRequest();

            await _service.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound();

            await _service.Remove(id);
            return Ok(result);
        }
    }
}
