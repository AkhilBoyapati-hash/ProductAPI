using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductAPI.Services.Interfaces; 

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("throw-error")]
        public IActionResult ThrowError()
        {
            // Simulate an exception
            throw new InvalidOperationException("This is a test error.");
        }
        // GET: api/products
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {

            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            await _productService.UpdateProductAsync(product);

            return NoContent(); 
        }



        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();  
            }

            await _productService.DeleteProductAsync(id);

            return NoContent(); 
        }


        // POST: api/products/{id}/decrement-stock
        [HttpPost("{id}/decrement-stock")]
        public async Task<IActionResult> DecrementStock(int id, [FromBody] int quantity)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();  
            }

            if (product.StockAvailable < quantity)
            {
                return BadRequest("Not enough stock to decrement");  
            }

            product.StockAvailable -= quantity;
            await _productService.UpdateProductAsync(product);
            return Ok();
        }

        // POST: api/products/{id}/increment-stock
        [HttpPost("{id}/increment-stock")]
        public async Task<IActionResult> IncrementStock(int id, [FromBody] int quantity)
        {
            // Fetch the product by ID
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();  // Return 404 if product is not found
            }

            product.StockAvailable += quantity;
            await _productService.UpdateProductAsync(product);
            return Ok(); 
        }

    }
}
