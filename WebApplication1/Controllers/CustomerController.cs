using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController(AppDbContaxt db)
        {
            _db = db;
        }
        private readonly AppDbContaxt _db;
    

    
        [HttpPost]
        public async Task<IActionResult> AddCustomers(Customers customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var P = await _db.Customers.ToListAsync();
            return Ok(P);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(p => p.Id == id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customers customer)
        {

            var p = await _db.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (p == null)
            {
                return NotFound($"Customer with ID {id} does not exist.");
            }

            p.Name = customer.Name;
            p.phonenumber = customer.phonenumber;
        

            await _db.SaveChangesAsync();
            return Ok(p);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> removecustomer(int id)
        {
            var c = await _db.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            { return NotFound($"customer id {id} not exist"); }
            _db.Customers.Remove(c);
            await _db.SaveChangesAsync();
            return Ok($"customer with id {id} deleted sucsessfully! ");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomerpath([FromBody] JsonPatchDocument<Customers> customer, [FromRoute] int id)
        {
            var c = await _db.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            { return NotFound($"Customer id {id} not exist"); }
            customer.ApplyTo(c, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _db.SaveChangesAsync();
            return Ok(c);
        }
    }
}