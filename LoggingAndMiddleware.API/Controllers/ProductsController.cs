using LoggingAndMiddleware.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LoggingAndMiddleware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ILogger<ProductsController> logger,ILoggerFactory loggerFactory) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var customILogger = loggerFactory.CreateLogger("CustomLogger");

            customILogger.LogInformation("This is a custom log message");
            logger.LogInformation("This is a log message");

            return Ok("Get all products");
        }

        [HttpPost]
        public IActionResult Post()
        {


            throw new ExceptionSaveToDatabase("Kritik Hata");
  
            return Ok("product created");
        }
    }
}
