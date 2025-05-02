using OpenWeather.Models;
using OpenWeather.Services;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder.Extensions;
using OpenWeather.Unit;


namespace OpenWeather.Test;
    public class OpenWeatherServiceTests
    {
        [Fact]
        public async Task GetWeather_ReturnsWeatherData_WhenAllDataIsAvailable()
        {
            var city = "Tehran";
            var location = OpenWeatherTestDataFactory.CreateLocation(city);
            var weather = OpenWeatherTestDataFactory.CreateWeather();
            var pollution = OpenWeatherTestDataFactory.CreatePollution();

            var factory = new OpenWeatherServiceTestFactory()
                .WithCity(city)
                .WithLocation(location)
                .WithWeather(weather)
                .WithPollution(pollution);

            var service = factory.BuildService();

            // Act
            var result = await service.GetWeather(city);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(location.Lat, result.Latitude);
            Assert.Equal(location.Lon, result.Longitude);
            Assert.Equal(weather.Main.Temp, result.Temperature);
            Assert.Equal(weather.Main.Feels_Like, result.FeelsLike);
            Assert.Equal(weather.Main.Humidity, result.Humidity);
            Assert.Equal(pollution.List.First().Main.Aqi, result.Aqi);
        }


        [Fact]
        public async Task GetWeather_ReturnsNull_WhenCityLocationIsNull()
        {
            var city = "UnkCity";
            var factory = new OpenWeatherServiceTestFactory()
                .WithCity(city)
                .WithLocation(null);

            var service = factory.BuildService();

            // Act
            var result = await service.GetWeather(city);

            // Assert
            Assert.Null(result);
            factory.MockLogger.Received().Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Is<object>(o => o.ToString() == $"City location not found or no data returned for city '{city}'"),
                null,
                Arg.Any<Func<object, Exception?, string>>());

        }

        [Fact]
        public async Task GetWeather_ReturnsNull_WhenWeatherIsNull()
        {
            var city = "Tehran";
            var location = OpenWeatherTestDataFactory.CreateLocation(city);

            var factory = new OpenWeatherServiceTestFactory()
                .WithCity(city)
                .WithLocation(location)
                .WithWeather(null);

            var service = factory.BuildService();

            // Act
            var result = await service.GetWeather(city);

            // Assert
            Assert.Null(result);
            factory.MockLogger.Received().Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Is<object>(o =>
                    o.ToString() == $"Weather data not found or empty for city '{city}' at coordinates [Lat: {location.Lat}, Lon: {location.Lon}]"),
                null,
                Arg.Any<Func<object, Exception?, string>>());

        }

        [Fact]
        public async Task GetWeather_ReturnsNull_WhenAirPollutionIsNull()
        {
            var city = "Tehran";
            var location = OpenWeatherTestDataFactory.CreateLocation(city);
            var weather = OpenWeatherTestDataFactory.CreateWeather();

            var factory = new OpenWeatherServiceTestFactory()
                .WithCity(city)
                .WithLocation(location)
                .WithWeather(weather)
                .WithPollution(null); 

            var service = factory.BuildService();

            // Act
            var result = await service.GetWeather(city);

            // Assert
            Assert.Null(result);
            factory.MockLogger.Received().Log(
                LogLevel.Warning,
                Arg.Any<EventId>(),
                Arg.Is<object>(o =>
                    o.ToString() == $"Air pollution data not found or empty for city '{city}' at coordinates [Lat: {location.Lat}, Lon: {location.Lon}]"),
                null,
                Arg.Any<Func<object, Exception?, string>>());

         
        }
    }
