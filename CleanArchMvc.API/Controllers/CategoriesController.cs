using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categories = await _service.GetCategories();
            if (categories == null)
                return NotFound("Categories not found");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _service.GetById(id);
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto category)
        {
            if (category == null)
                return BadRequest("Invalid data");

            await _service.Add(category);
            return new CreatedAtRouteResult("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDto category)
        {
            if (id != category.Id)
                return BadRequest();

            if (category == null)
                return BadRequest();

            await _service.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Delete(int id)
        {
            var category = await _service.GetById(id);
            if (category == null)
                return NotFound();

            await _service.Remove(id);
            return Ok(category);

        }
    }
}
