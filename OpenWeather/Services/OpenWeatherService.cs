using Microsoft.Extensions.Options;
using OpenWeather.Models;

namespace OpenWeather.Services
{
    public sealed class OpenWeatherService : IOpenWeatherService
    {
        private readonly IOpenWeatherHttpService _openWeatherHttpService;
        private readonly ILogger<OpenWeatherService> _logger;
        private readonly string _apiKey;

        public OpenWeatherService(IOpenWeatherHttpService openWeatherHttpService, IOptions<OpenWeatherOptions> options, ILogger<OpenWeatherService> logger)
        {
            _openWeatherHttpService = openWeatherHttpService;
            _apiKey = options.Value.ApiKey;
            _logger = logger;
        }

        public async Task<WeatherDataDto?> GetWeather(string city)
        {
            var cityLocation = await _openWeatherHttpService.GetCityDirectByName(city, _apiKey);
            if (cityLocation == null)
            {
                _logger.LogWarning("City location not found or no data returned for city '{City}'", city);
                return null;
            }

            var weather = await _openWeatherHttpService.GetCityWeather(cityLocation.Lat, cityLocation.Lon, _apiKey);
            if (weather == null)
            {
                _logger.LogWarning("Weather data not found or empty for city '{City}' at coordinates [Lat: {Lat}, Lon: {Lon}]", city, cityLocation.Lat, cityLocation.Lon);
                return null;
            }
            var airPollution = await _openWeatherHttpService.GetCityAirPollution(cityLocation.Lat, cityLocation.Lon, _apiKey);
            if (airPollution == null)
            {
                _logger.LogWarning("Air pollution data not found or empty for city '{City}' at coordinates [Lat: {Lat}, Lon: {Lon}]", city, cityLocation.Lat, cityLocation.Lon);
                return null;
            }

            var weatherData = MapToWeatherDataDto(cityLocation, weather, airPollution);

            return weatherData;
        }

        private WeatherDataDto MapToWeatherDataDto(CityLocationResponseDto cityLocation, WeatherResponseDto weather, AirPollutionResponseDto airPollution)
        {
            return new WeatherDataDto
            {
                Latitude = cityLocation.Lat,
                Longitude = cityLocation.Lon,
                Temperature = weather.Main.Temp,
                FeelsLike = weather.Main.Feels_Like,
                Humidity = weather.Main.Humidity,
                WindSpeed = weather.Wind?.Speed ?? 0,
                Aqi = airPollution.List.FirstOrDefault()?.Main.Aqi ?? 0,
                MajorPollutants = new Dictionary<string, double>
        {
            { "CO", airPollution.List.FirstOrDefault()?.Components.Co ?? 0 },
            { "NO", airPollution.List.FirstOrDefault()?.Components.No ?? 0 },
            { "NO2", airPollution.List.FirstOrDefault()?.Components.No2 ?? 0 },
            { "O3", airPollution.List.FirstOrDefault()?.Components.O3 ?? 0 },
            { "SO2", airPollution.List.FirstOrDefault()?.Components.So2 ?? 0 },
            { "PM2.5", airPollution.List.FirstOrDefault()?.Components.Pm2_5 ?? 0 },
            { "PM10", airPollution.List.FirstOrDefault()?.Components.Pm10 ?? 0 },
            { "NH3", airPollution.List.FirstOrDefault()?.Components.Nh3 ?? 0 }
        }
            };
            
        }
    }
}
