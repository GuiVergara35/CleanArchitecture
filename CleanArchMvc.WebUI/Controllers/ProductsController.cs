using System.IO;
using System.Threading.Tasks;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _catService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService service, ICategoryService catService,
                                    IWebHostEnvironment environment)
        {
            _service = service;
            _catService = catService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetProducts();
            return View(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _catService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _service.GetById(id);
            if (productDto == null)
                return NotFound();

            var categories = await _catService.GetCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _service.Update(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _service.GetById(id);
            if (productDto == null)
                return NotFound();

            return View(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Remove(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _service.GetById(id);
            if (productDto == null)
                return NotFound();

            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDto);
        }
    }
}
