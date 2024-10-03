using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.Services.ProductService;
using Store.Service.Services.ProductService.Dtos;

namespace Store.Web.Helper
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProuductService _prouductService;

        public ProductController(IProuductService prouductService)
        {
            _prouductService = prouductService;
        }
         
        [HttpGet] 
        //[HttpGet("GetAllBrands")]  you should use the name of method in the flage if you dosent use [action] in the route on the top
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
        => Ok(await _prouductService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
        => Ok(await _prouductService.GetAllTypesAsync());

        [HttpGet]
        [Cashe (20)]
        public async Task<ActionResult<IReadOnlyList<ProuductDetailsDto>>> GetAllProducts( [FromQuery] ProductSpecification input) 
         => Ok(await _prouductService.GetAllProductAsync(input));

        [HttpGet]
        public async Task<ActionResult<ProuductDetailsDto>> GetProductsById(int? id)
        => Ok(await _prouductService.GetProductByIdAsync(id));
    }
}
