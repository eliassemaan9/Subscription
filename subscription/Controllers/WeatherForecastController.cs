using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace subscription.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
       

        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "healthCheck")]
        public string Get()
        {
            return "healthy";
        }
    }
}