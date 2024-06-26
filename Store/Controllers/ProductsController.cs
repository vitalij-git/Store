using API.Dto;
using API.Errors;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> brandsRepo, 
            IGenericRepository<ProductType> typesRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsAsync()
        {
            var spec = new ProductWithTypesAndBrandSpecification();
            var products = await _productsRepo.GetListAsync(spec);
            var productToReturnDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(productToReturnDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductAsync(int id)
        {
            var spec = new ProductWithTypesAndBrandSpecification(id);
            var product = await _productsRepo.GetEntitywithSpec(spec);
            var productToReturnDto = _mapper.Map<Product, ProductToReturnDto>(product);

            if (productToReturnDto == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(productToReturnDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync() 
        {
            var spec = new ProductWithTypesAndBrandSpecification();
            var brands = await _brandsRepo.GetListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
            var types = await _typesRepo.GetListAllAsync();
            return Ok(types);
        }
    }
}
