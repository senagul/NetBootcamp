using Bootcamp.Service.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Service.Weather
{
    public interface IWeatherService
    {
        ResponseModelDto<int> GetWeather(string city);
    }

    public class WeatherService : IWeatherService
    {
        public ResponseModelDto<int> GetWeather(string city)
        {
            return ResponseModelDto<int>.Success(20);
        }
    }
}
