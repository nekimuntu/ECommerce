using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //On declare le private readonly StoreContext poour avoir acces a la DB
       private readonly StoreContext _context;
        //On cree le construteur de Products pour quil initialise la connexion a la 
        //DB lorsqu il est cree. On utilise le context precedemment Initialis
        public ProductsController(StoreContext context)
        {
            _context = context;
            
        }
        //Summary: On utilise async pour eviter que la methode attende la DB 
        // ToListAsync est une fonction a utiliser avec using Microsoft.EntityFrameworkCore;
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOneProduct(int id){
            return await _context.Products.FindAsync(id);
        }
    }
}