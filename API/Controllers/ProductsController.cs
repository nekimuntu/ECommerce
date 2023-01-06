using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private  IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private object productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
                    IGenericRepository<ProductBrand> productBrandRepo,
                    IGenericRepository<ProductType> productTypeRepo)
        {
            _productsRepo = productsRepo;
            _productBrandRepo =productBrandRepo;
            _productTypeRepo=productTypeRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandSpecification();

            var products = await _productsRepo.ListAsync(spec);
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetOneProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            };
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