using OpenWeather.Models;

namespace OpenWeather.Services
{
    public interface IOpenWeatherService
    {
        Task<WeatherDataDto> GetWeather(string city);
    }
}
