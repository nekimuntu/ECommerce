using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private  IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        private object productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
                    IGenericRepository<ProductBrand> productBrandRepo,
                    IGenericRepository<ProductType> productTypeRepo,
                    IMapper mapper)
        {
            this._mapper = mapper;
            _productsRepo = productsRepo;
            _productBrandRepo =productBrandRepo;
            _productTypeRepo=productTypeRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandSpecification();

            var products = await _productsRepo.ListAsync(spec);
            
            return products.Select(product => new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            }).ToList();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetOneProduct(int Id)
        {
            var spec = new ProductsWithTypesAndBrandSpecification(Id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }
        
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {

            var types = await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }
    }
}