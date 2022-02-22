using Microsoft.AspNetCore.Mvc;

namespace Okta_ClientFlowDotNetSix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Okta.IJwtValidator _validationService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Okta.IJwtValidator validationService)
        {
            _logger = logger;
            _validationService = validationService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var authToken = this.HttpContext.Request.Headers["Authorization"].ToString();

            if (String.IsNullOrEmpty(authToken))
            {
                return Unauthorized();
            }

            var validatedToken = await _validationService.ValidateToken(authToken.Split(" ")[1]);

            if(validatedToken == null)
            {
                return Unauthorized();
            }

            return new JsonResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}