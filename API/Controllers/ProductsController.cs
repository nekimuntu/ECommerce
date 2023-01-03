using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //On declare le private readonly StoreContext poour avoir acces a la DB
        private readonly IProductRepository _repo;
        //On cree le construteur de Products pour quil initialise la connexion a la 
        //DB lorsqu il est cree. On utilise le context precedemment Initialis
        /// ... IProductRepository context est maintenat utilise a la place du contexte... 2eme niveau d abstraction
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;

        }
        //Summary: On utilise async pour eviter que la methode attende la DB 
        // ToListAsync est une fonction a utiliser avec using Microsoft.EntityFrameworkCore;
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            var products = await _repo.GetProductsAsync() ;
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOneProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _repo.GetProductsBrandAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {

            var types = await _repo.GetProductsTypeAsync();
            return Ok(types);
        }
    }
}