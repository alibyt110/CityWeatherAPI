using Microsoft.AspNetCore.Mvc.Formatters;
using OpenWeather.Models;
using System.Net.Http;
using System.Text.Json;

namespace OpenWeather.Services;

    public sealed class OpenWeatherHttpService : IOpenWeatherHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenWeatherHttpService> _logger;
        public OpenWeatherHttpService(IHttpClientFactory httpClientFactory, ILogger<OpenWeatherHttpService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");
            _logger = logger;
        }

        public async Task<CityLocationResponseDto> GetCityDirectByName(string city,string apiKey)
        {
            var url = $"geo/1.0/direct?q={city}&limit=1&appid={apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to get city data: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var locations = JsonSerializer.Deserialize<List<CityLocationResponseDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return locations?.FirstOrDefault();
        }

        public async Task<WeatherResponseDto> GetCityWeather(double lat, double lon, string apikey)
        {
            var url = $"data/2.5/weather?lat={lat}&lon={lon}&appid={apikey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to get GetCityWeather data: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var WeatherResponseDto = JsonSerializer.Deserialize<WeatherResponseDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return WeatherResponseDto;
        }


        public async Task<AirPollutionResponseDto> GetCityAirPollution(double lat, double lon, string apikey)
        {
            var url = $"data/2.5/air_pollution?lat={lat}&lon={lon}&appid={apikey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to get GetCityAirPollution data: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var AirPollutionResponseDto = JsonSerializer.Deserialize<AirPollutionResponseDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return AirPollutionResponseDto;
        }



    }


