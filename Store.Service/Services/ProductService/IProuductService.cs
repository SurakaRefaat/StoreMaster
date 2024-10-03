using Store.Repository.Specification.ProductSpecs;
using Store.Service.Services.Helper;
using Store.Service.Services.ProductService.Dtos;

namespace Store.Service.Services.ProductService
{
    public interface IProuductService
    {
        Task<ProuductDetailsDto> GetProductByIdAsync(int? productId);
        Task<PaginatedResultDto<ProuductDetailsDto>> GetAllProductAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();

    }
}
