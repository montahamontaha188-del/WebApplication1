using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.models;
using System.ComponentModel;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(AppDbContaxt db)
        {
            _db = db;
        }
        private readonly AppDbContaxt _db;


        [HttpPost]
        public async Task<IActionResult> AddProduct(Products product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetPoducts()
        {
            var P = await _db.Products.ToListAsync();
            return Ok(P);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _db.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id,[FromBody] Products product)
        {
           
            var p = await _db.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (p == null)
            {
                return NotFound($"Product with ID {id} does not exist.");
            }
 
            p.Name = product.Name;
            p.Price = product.Price;
            p.quaintity = product.quaintity;

            await _db.SaveChangesAsync();
            return Ok(p);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> removeproduct(int id)
        {
            var c = await _db.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            { return NotFound($"product id {id} not exist"); }
            _db.Products.Remove(c);
            await _db.SaveChangesAsync();
            return Ok($"Product with id {id} deleted sucsessfully! ");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategoryPatch([FromBody] JsonPatchDocument<Products> product, [FromRoute] int id)
        {
            var c = await _db.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            { return NotFound($"Product id {id} not exist"); }
            product.ApplyTo(c, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _db.SaveChangesAsync();
            return Ok(c);
        }
        [HttpGet("totals")]
        public IActionResult GetCategoryTotals()
        {
            var report = _db.Products
                .GroupBy(p => p.Category)  
                .Select(g => new {
                    CategoryName = g.Key.ToString(),
                    TotalValue = g.Sum(p => p.Price * p.quaintity)   
                })
                .ToList();

            return Ok(report);
        }


    }
   
            
}
        
 
