namespace OpenWeather.Models
{
    public class CityLocationResponseDto
    {
        public string Name { get; set; }
        public Dictionary<string, string> LocalNames { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
    }
}
