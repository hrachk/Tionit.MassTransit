using Microsoft.AspNetCore.Mvc;
using Tionit.MassTransit.API.Entities;
using Tionit.MassTransit.Consumer.Models;

namespace Tionit.MassTransit.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("customers")]
        public async Task<IActionResult> Get()
        {
            var customers = _context.Customers.ToList();             

            return new JsonResult(customers);// Ok(customerName);
        }

        [HttpPost]
        [Route("add_customer")]
        public async Task<IActionResult> Add(CustomerModel customer)
        {
            _context.Add(new CustomerModel  { CustomerName = customer.CustomerName, CustomerAge = customer.CustomerAge });
            await _context.SaveChangesAsync();

            return Ok(StatusCodes.Status201Created);
        }
    }
}