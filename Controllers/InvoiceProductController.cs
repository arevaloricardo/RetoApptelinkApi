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
    public class InvoiceProductController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InvoiceProductController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceProduct
        [HttpGet]
        public async Task<IActionResult> GetInvoicesProducts(string filterByProperty, string filterValue, string orderByProperty, string sortOrder, int pageIndex, int pageSize)
        {
            var invoicesProducts = _context.InvoicesProducts.AsQueryable();

            if (!string.IsNullOrEmpty(filterByProperty) && !string.IsNullOrEmpty(filterValue))
            {
                if (int.TryParse(filterValue, out int intValue) && filterByProperty == "Id_InvoiceProduct")
                {
                    invoicesProducts = invoicesProducts.Where(i => i.Id_InvoiceProduct == intValue);
                }
                else
                {
                    invoicesProducts = invoicesProducts.FilterBy(filterByProperty, filterValue);
                }
            }

            if (!string.IsNullOrEmpty(orderByProperty))
            {
                invoicesProducts = invoicesProducts.OrderBy(orderByProperty, sortOrder);
            }

            var paginatedList = await PaginatedList<InvoiceProduct>.CreateAsync(invoicesProducts, pageIndex, pageSize);

            var invoices = await _context.Invoices.ToDictionaryAsync(i => i.Id_Invoice, i => i);

            var result = paginatedList.Select(i => new
            {
                i.Id_InvoiceProduct,
                i.Quantity_InvoiceProduct,
                i.Id_Invoice_InvoiceProduct,
                Invoice = invoices.ContainsKey(i.Id_Invoice_InvoiceProduct) ? invoices[i.Id_Invoice_InvoiceProduct] : null,
                i.Id_Product_InvoiceProduct
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


        // GET: api/InvoiceProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceProduct>> GetInvoiceProduct(int id)
        {
            var invoiceProduct = await _context.InvoicesProducts.FindAsync(id);

            if (invoiceProduct == null)
            {
                return NotFound();
            }

            return invoiceProduct;
        }

        // PUT: api/InvoiceProduct/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceProduct(int id, InvoiceProduct invoiceProduct)
        {
            if (id != invoiceProduct.Id_InvoiceProduct)
            {
                return BadRequest();
            }

            _context.Entry(invoiceProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceProductExists(id))
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

        // POST: api/InvoiceProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceProduct>> PostInvoiceProduct(InvoiceProduct invoiceProduct)
        {
            _context.InvoicesProducts.Add(invoiceProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceProduct", new { id = invoiceProduct.Id_InvoiceProduct }, invoiceProduct);
        }

        // DELETE: api/InvoiceProduct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceProduct(int id)
        {
            var invoiceProduct = await _context.InvoicesProducts.FindAsync(id);
            if (invoiceProduct == null)
            {
                return NotFound();
            }

            _context.InvoicesProducts.Remove(invoiceProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceProductExists(int id)
        {
            return _context.InvoicesProducts.Any(e => e.Id_InvoiceProduct == id);
        }
    }
}
