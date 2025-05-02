using Microsoft.AspNetCore.Mvc;
using OpenWeather.Services;

namespace OpenWeather.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly ILogger<WeatherController> _logger;
        public WeatherController(IOpenWeatherService openWeatherService, ILogger<WeatherController> logger)
        {
            _openWeatherService = openWeatherService;
            _logger = logger;
        }



        [HttpGet("{city}")]
        public async Task<IActionResult> Weather(string city)
        {

            var weatherData = await _openWeatherService.GetWeather(city);
            if (weatherData == null)
                return NotFound($"City '{city}' not found");

            return Ok(weatherData);
        }
    }
}
