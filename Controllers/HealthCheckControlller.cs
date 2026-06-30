using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace trainee_management.Controllers;

[ApiController]
[Route("api/health")]
public class HealthCheckController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthCheckController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet("check")]
    public async Task<IActionResult> GetSystemReport()
    {   
        HealthReport report = await _healthCheckService.CheckHealthAsync();
        var customResponse = new
        {
            OverallStatus = report.Status.ToString(), 
            CheckedAt = DateTime.UtcNow,
            Dependencies = report.Entries.Select(entry => new
            {
                Component = entry.Key,
                Status = entry.Value.Status.ToString(),
                DurationMs = entry.Value.Duration.TotalMilliseconds,
                ErrorMessage = entry.Value.Exception?.Message 
            })
        };

        if (report.Status == HealthStatus.Unhealthy)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, customResponse);
        }

        return Ok(customResponse);
    }
}
