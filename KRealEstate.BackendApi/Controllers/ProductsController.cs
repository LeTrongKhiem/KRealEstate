using KRealEstate.Application.Catalog.Products;
using KRealEstate.ViewModels.Catalog.Assigns;
using KRealEstate.ViewModels.Catalog.Images;
using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Catalog.Products;
using KRealEstate.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] PagingProduct request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.GetAllPaging(request);
            if (result == null)
            {
                return BadRequest("Not found");
            }
            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        //[Consumes("mutilpart/form-data")]
        public async Task<IActionResult> Create([FromForm] PostProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.PostProduct(request);
            if (result == null)
            {
                return BadRequest(request);
            }
            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string id)
        {
            var request = new DeletePostProductRequest()
            {
                Id = id
            };
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.DeletePostProduct(request);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.GetById(id);
            if (result == null)
            {
                return BadRequest("Not found");
            }
            return Ok(result);
        }
        [HttpGet("slug")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug([FromQuery] string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.GetBySlug(slug);
            if (result == null)
            {
                return BadRequest(slug);
            }
            return Ok(result);
        }
        [HttpPut("categories/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> CategoryAssign(string id, CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.CategoryAssign(id, request);
            if (!result)
            {
                return BadRequest(request);
            }
            return Ok(result);
        }
        [HttpGet("images/getlist/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListImages(string productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.GetListImage(productId);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("images/{productId}/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateImages(string productId, [FromForm] CreateImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CreateImageRequest re = new CreateImageRequest()
            {
                ProductId = productId,
                ImageId = request.ImageId,
                Images = request.Images,
                IsThumbnail = request.IsThumbnail,
                Path = request.Path
            };
            var result = await _productService.CreateImages(re);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("images/update/{imageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateImages(string imageId, [FromForm] UpdateImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.UpdateImages(imageId, request);
            if (result == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
