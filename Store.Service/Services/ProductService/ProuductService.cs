using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.ProductSpecs;
using Store.Service.Services.Helper;
using Store.Service.Services.ProductService.Dtos;

namespace Store.Service.Services.ProductService
{
    public class ProuductService : IProuductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProuductService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();

            //var mappedBrands = brands.Select(X => new BrandTypeDetailsDto
            //{
            //    Id = X.Id,
            //    Name = X.Name,
            //    CreateAt = DateTime.Now,
            //}).ToList();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);

            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProuductDetailsDto>> GetAllProductAsync(ProductSpecification input )
        {

            var specs = new ProductWithSpecification ( input );

            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);

            var countSpecs = new productWithCountSpecification(input);

            var count = await _unitOfWork.Repository < Product ,int>().GetCountSpecificationAsync(countSpecs);

            //var mappedProducts = products.Select(X => new ProuductDetailsDto

            //{ 
            //    Id=X.Id,
            //    Name = X.Name,  
            //    Description = X.Description,
            //    pictureUrl = X.PictureUrl,
            //    Price = X.Price,
            //    Brandname = X.Brand.Name,
            //    TypeName = X.Type.Name, 
            //    CreateAt = DateTime.Now,
            //}).ToList();

            var mappedProducts = _mapper.Map<IReadOnlyList<ProuductDetailsDto>>(products);



            return new PaginatedResultDto<ProuductDetailsDto>(input.PageIndex,input.PageSize, count, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            //var mappedtypes = types.Select(X => new BrandTypeDetailsDto
            //{
            //    Id = X.Id,
            //    Name = X.Name,
            //    CreateAt = DateTime.Now,
            //}).ToList();

            var mappedtypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);

            return mappedtypes;
        }

        public async Task<ProuductDetailsDto> GetProductByIdAsync(int? productId)
        {
            if (productId is null)
                throw new Exception("Id is null");

            var specs = new ProductWithSpecification(productId);

            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecificationByIdAsync(specs);

            if (product is null)
                throw new Exception("product Not Found");

            //var mappedProduct = new ProuductDetailsDto
            //{
            //    Id = productId.Value,
            //    Name = product.Name,
            //    CreateAt = product.CreatedAt,
            //    Description = product.Description,
            //    Price = product.Price,
            //    pictureUrl = product.PictureUrl,
            //    Brandname = product.Brand.Name,
            //    TypeName = product.Type.Name
            //};

            var mappedProduct = _mapper.Map<ProuductDetailsDto>(product);
             return mappedProduct;
        }
    }
}
