using OpenWeather.Models;

namespace OpenWeather.Services;
    public interface IOpenWeatherHttpService
    {
        Task<CityLocationResponseDto> GetCityDirectByName(string city, string apiKey);
        Task<WeatherResponseDto> GetCityWeather(double lat, double log, string apikey);
        Task<AirPollutionResponseDto> GetCityAirPollution(double lat, double log, string apikey);
    }
