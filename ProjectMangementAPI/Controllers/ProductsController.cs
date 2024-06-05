using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Repository;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _repository = new ProductRepository();
        
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => _repository.GetProducts();
        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int id) => _repository.GetProduct(id);
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("id")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var pTmp = _repository.GetProduct(id);
            if (pTmp == null)
            {
                return NotFound();
            }
            _repository.UpdateProduct(product);
            
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
           _repository.SaveProduct(product);
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var p = _repository.GetProduct(id);
            if (p == null)
            {
                return NotFound();
            }
            _repository.DeleteProduct(p);
            return NoContent();
        }

    }
}
