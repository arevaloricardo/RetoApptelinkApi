using System;
using System.Linq;
using System.Threading.Tasks;
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
    public class InvoiceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InvoiceController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetInvoices(string filterByProperty, string filterValue, string orderByProperty, string sortOrder, int pageIndex, int pageSize)
        {
            var invoices = _context.Invoices.AsQueryable();

            if (!string.IsNullOrEmpty(filterByProperty) && !string.IsNullOrEmpty(filterValue))
            {
                if (int.TryParse(filterValue, out int intValue) && filterByProperty == "Id_Invoice")
                {
                    invoices = invoices.Where(i => i.Id_Invoice == intValue);
                }
                else
                {
                    invoices = invoices.FilterBy(filterByProperty, filterValue);
                }
            }

            if (!string.IsNullOrEmpty(orderByProperty))
            {
                invoices = invoices.OrderBy(orderByProperty, sortOrder);
            }

            var paginatedList = await PaginatedList<Invoice>.CreateAsync(invoices, pageIndex, pageSize);

            var customers = await _context.Customer.ToDictionaryAsync(c => c.Id_Customer, c => c);

            var result = paginatedList.Select(i => new
            {
                i.Id_Invoice,
                i.Total_Invoice,
                i.Id_Customer_Invoice,
                Customer = customers.ContainsKey(i.Id_Customer_Invoice) ? customers[i.Id_Customer_Invoice] : null,
                i.Date_Created_Invoice
            }).ToList();

            // Si la cantidad de elementos es igual al tamaño de la página, entonces hay una página siguiente
            var hasNextPage = paginatedList.Count == pageSize;

            var response = new
            {
                statusCode = (int)HttpStatusCode.OK,
                dataCount = result.Count,
                data = result,
                hasNextPage = hasNextPage // Agrega esta propiedad a la respuesta
            };

            return Ok(response);
        }




        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.Id_Invoice)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id_Invoice }, invoice);
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id_Invoice == id);
        }
    }
}
