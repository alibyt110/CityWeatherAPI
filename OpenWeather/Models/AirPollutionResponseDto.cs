
namespace OpenWeather.Models
{
    public class AirPollutionResponseDto
    {
        public Coord Coord { get; set; }
        public List<AirData> List { get; set; }
    }



    public class AirData
    {
        public AirQualityMain Main { get; set; }
        public Components Components { get; set; }
        public long Dt { get; set; }
    }

    public class AirQualityMain
    {
        public int Aqi { get; set; } 
    }

    public class Components
    {
        public double Co { get; set; }
        public double No { get; set; }
        public double No2 { get; set; }
        public double O3 { get; set; }
        public double So2 { get; set; }
        public double Pm2_5 { get; set; }
        public double Pm10 { get; set; }
        public double Nh3 { get; set; }
    }
}
