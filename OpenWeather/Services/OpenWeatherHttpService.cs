using Microsoft.AspNetCore.Mvc.Formatters;
using OpenWeather.Models;
using System.Net.Http;
using System.Text.Json;

namespace OpenWeather.Services;

public sealed class OpenWeatherHttpService : IOpenWeatherHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenWeatherHttpService> _logger;
    private readonly OpenWeatherApiEndpoints _endpoints;

    public OpenWeatherHttpService(IHttpClientFactory httpClientFactory, ILogger<OpenWeatherHttpService> logger, OpenWeatherApiEndpoints endpoints)
    {
        _httpClient = httpClientFactory.CreateClient();
        _endpoints = endpoints;
        _httpClient.BaseAddress = new Uri(_endpoints.BaseAddress);
        _logger = logger;
    }

    public async Task<CityLocationResponseDto> GetCityDirectByName(string city, string apiKey)
    {
        var url =  _endpoints.GetGeoCodingUrl(city, apiKey);

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
        var url = _endpoints.GetWeatherUrl(lat, lon, apikey);
     
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
        var url = _endpoints.GetAirPollutionUrl(lat, lon, apikey);

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


