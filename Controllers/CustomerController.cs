using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoApptelinkApi.Data;
using RetoApptelinkApi.Models;
using RetoApptelinkApi.Helpers; 
using System.Net;

namespace RetoApptelinkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CustomerController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetCustomers(string filterByProperty, string filterValue, string orderByProperty, string sortOrder, int pageIndex, int pageSize)
        {
            var customers = _context.Customer.AsQueryable();

            if (!string.IsNullOrEmpty(filterByProperty) && !string.IsNullOrEmpty(filterValue))
            {
                if (int.TryParse(filterValue, out int intValue) && filterByProperty == "Id_Customer")
                {
                    customers = customers.Where(c => c.Id_Customer == intValue);
                }
                else
                {
                    customers = customers.FilterBy(filterByProperty, filterValue);
                }
            }

            if (!string.IsNullOrEmpty(orderByProperty))
            {
                customers = customers.OrderBy(orderByProperty, sortOrder);
            }

            var paginatedList = await PaginatedList<Customer>.CreateAsync(customers, pageIndex, pageSize);

            // Si la cantidad de elementos es igual al tamaño de la página, entonces hay una página siguiente
            var hasNextPage = paginatedList.Count == pageSize;

            var response = new
            {
                statusCode = (int)HttpStatusCode.OK,
                dataCount = paginatedList.Count,
                data = paginatedList,
                hasNextPage = hasNextPage // Agrega esta propiedad a la respuesta
            };

            return Ok(response);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id_Customer)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer.RucDni_Customer.Length > 15)
            {
                return BadRequest(new { message = "El Ruc/Dni no puede ser mayor que 15 caracteres." });
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id_Customer }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id_Customer == id);
        }
    }
}
