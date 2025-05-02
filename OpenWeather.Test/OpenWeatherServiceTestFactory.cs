using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using OpenWeather.Models;
using OpenWeather.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Unit;
    public class OpenWeatherServiceTestFactory
    {
        private readonly IOpenWeatherHttpService _mockHttpService = Substitute.For<IOpenWeatherHttpService>();
        private readonly ILogger<OpenWeatherService> _mockLogger = Substitute.For<ILogger<OpenWeatherService>>();
        private readonly IOptions<OpenWeatherOptions> _options = Substitute.For<IOptions<OpenWeatherOptions>>();
        private string _apiKey = "test";
        private string _city = "TestCity";
        private CityLocationResponseDto? _location;
        private WeatherResponseDto? _weather;
        private AirPollutionResponseDto? _pollution;

        public ILogger<OpenWeatherService> MockLogger => _mockLogger; // expose logger for assertion

        public OpenWeatherServiceTestFactory WithCity(string city)
        {
            _city = city;
            return this;
        }

        public OpenWeatherServiceTestFactory WithLocation(CityLocationResponseDto? location)
        {
            _location = location;
            return this;
        }

        public OpenWeatherServiceTestFactory WithWeather(WeatherResponseDto? weather)
        {
            _weather = weather;
            return this;
        }

        public OpenWeatherServiceTestFactory WithPollution(AirPollutionResponseDto? pollution)
        {
            _pollution = pollution;
            return this;
        }

        public OpenWeatherService BuildService()
        {
            _options.Value.Returns(new OpenWeatherOptions { ApiKey = _apiKey });

            _mockHttpService
                .GetCityDirectByName(_city, _apiKey)
                .Returns(Task.FromResult(_location));

            if (_location != null)
            {
                _mockHttpService
                    .GetCityWeather(_location.Lat, _location.Lon, _apiKey)
                    .Returns(Task.FromResult(_weather));

                if (_weather != null)
                {
                    _mockHttpService
                        .GetCityAirPollution(_location.Lat, _location.Lon, _apiKey)
                        .Returns(Task.FromResult(_pollution));
                }
            }

            return new OpenWeatherService(_mockHttpService, _options, _mockLogger);
        }
    }

