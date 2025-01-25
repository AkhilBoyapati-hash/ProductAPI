using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("throw-error")]
        public IActionResult ThrowError()
        {
            // Simulate an exception
            throw new InvalidOperationException("This is a test error.");
        }
    }

}
