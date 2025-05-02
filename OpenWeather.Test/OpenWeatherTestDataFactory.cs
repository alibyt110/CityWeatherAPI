using OpenWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Unit;
    public static class OpenWeatherTestDataFactory
    {
        public static CityLocationResponseDto CreateLocation(string city = "Tehran", double lat = 35.69, double lon = 51.39)
        {
            return new CityLocationResponseDto
            {
                Name = city,
                Lat = lat,
                Lon = lon
            };
        }

        public static WeatherResponseDto CreateWeather(double temp = 25, double feelsLike = 23, int humidity = 60, string description = "Clear")
        {
            return new WeatherResponseDto
            {
                Main = new WeatherMain
                {
                    Temp = temp,
                    Feels_Like = feelsLike,
                    Humidity = humidity
                },
                Weather = new List<Weather>
            {
                new Weather { Description = description }
            },
                Wind = new Wind { Speed = 3.5 }
            };
        }

        public static AirPollutionResponseDto CreatePollution(int aqi = 2)
        {
            return new AirPollutionResponseDto
            {
                List = new List<AirData>
            {
                new AirData
                {
                    Main = new AirQualityMain { Aqi = aqi },
                    Components = new Components
                    {
                        Co = 0,
                        No = 0,
                        No2 = 0,
                        O3 = 0,
                        So2 = 0,
                        Pm2_5 = 0,
                        Pm10 = 0,
                        Nh3 = 0
                    }
                }
            }
            };
        }
    }

