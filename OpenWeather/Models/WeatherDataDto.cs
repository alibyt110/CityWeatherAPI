namespace OpenWeather.Models
{
    public class WeatherDataDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }

        public int Aqi { get; set; }
        public Dictionary<string, double> MajorPollutants { get; set; }
    }
}
