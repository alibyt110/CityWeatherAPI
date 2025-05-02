namespace OpenWeather.Services
{
    public class OpenWeatherApiEndpoints
    {
        public string BaseAddress { get; } = "https://api.openweathermap.org/";
        public string GeoCoding { get; } = "geo/1.0/direct";
        public string Weather { get; } = "data/2.5/weather";
        public string AirPollution { get; } = "data/2.5/air_pollution";

        public string GetGeoCodingUrl(string city, string apiKey, int limit = 1)
            => $"{GeoCoding}?q={city}&limit={limit}&appid={apiKey}";

        public string GetWeatherUrl(double lat, double lon, string apiKey)
            => $"{Weather}?lat={lat}&lon={lon}&appid={apiKey}";

        public string GetAirPollutionUrl(double lat, double lon, string apiKey)
            => $"{AirPollution}?lat={lat}&lon={lon}&appid={apiKey}";
    }
}
