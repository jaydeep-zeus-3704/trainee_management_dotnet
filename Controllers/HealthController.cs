using Microsoft.AspNetCore.Mvc;
namespace trainee_management.Controllers;

    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status="running",
                application="trainee_management_api",
                timestamp=DateTime.UtcNow
            });
        }
    }

