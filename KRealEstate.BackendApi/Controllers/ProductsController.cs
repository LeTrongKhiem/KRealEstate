using KRealEstate.Application.Catalog.Products;
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
    }
}
