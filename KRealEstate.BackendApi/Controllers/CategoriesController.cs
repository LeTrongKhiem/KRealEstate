using KRealEstate.Application.Catalog.Categories;
using KRealEstate.ViewModels.Catalog.Categories;
using KRealEstate.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("slug")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _categoryService.GetBySlug(slug);
            return Ok(result);
        }
        [HttpPost]
        [ActionName(nameof(Create))]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cateId = await _categoryService.Create(request);
            if (cateId == null || cateId.Equals(""))
            {
                return BadRequest(request);
            }
            var cate = await _categoryService.GetById(cateId);
            return CreatedAtAction(nameof(Create), new { id = cateId }, cate);
        }
        [HttpGet]

        public async Task<IActionResult> GetAllPaging([FromQuery] PagingPageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _categoryService.GetAll(request);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, CategoryEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _categoryService.Edit(id, request);
            if (!result)
            {
                return BadRequest(request);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var cateId = new CategoryDeleteRequest()
            {
                Id = id
            };
            var result = await _categoryService.Delete(cateId);
            if (!result)
            {
                return BadRequest(id);
            }
            return Ok(result);
        }
    }
}
