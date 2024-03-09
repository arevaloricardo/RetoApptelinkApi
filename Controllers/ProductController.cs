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
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProducts(string filterByProperty, string filterValue, string orderByProperty, string sortOrder, int pageIndex, int pageSize)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filterByProperty) && !string.IsNullOrEmpty(filterValue))
            {
                if (int.TryParse(filterValue, out int intValue) && filterByProperty == "Id_Product")
                {
                    products = products.Where(p => p.Id_Product == intValue);
                }
                else
                {
                    products = products.FilterBy(filterByProperty, filterValue);
                }
            }

            if (!string.IsNullOrEmpty(orderByProperty))
            {
                products = products.OrderBy(orderByProperty, sortOrder);
            }

            var paginatedList = await PaginatedList<Product>.CreateAsync(products, pageIndex, pageSize);

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

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id_Product)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id_Product }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id_Product == id);
        }
    }
}
