using Bootcamp.Web.Models;
using Bootcamp.Web.TokenServices;
using System.Net;
using System.Net.Http.Headers;

namespace Bootcamp.Web.WeatherServices
{
    public class WeatherService(HttpClient httpClient, TokenService tokenService, ILogger<WeatherService> logger)
    {
        public async Task<ServiceResponseModel<int>> GetWeatherForecastWithCity(string cityName)
        {

            var response = await httpClient.GetAsync($"/api/weather?city={cityName}");


            var responseAsBody = await response.Content.ReadFromJsonAsync<ResponseModelDto<int>>();

            if(!response.IsSuccessStatusCode)
            {
                return ServiceResponseModel<int>.Fail(responseAsBody!.FailMessages!);
            }

            return ServiceResponseModel<int>.Success(responseAsBody!.Data);

        }

        public async Task<string> GetWeatherForecastWithCityBetter(string cityName)
        {

            var response = await httpClient.GetAsync($"/api/weather?city={cityName}");


            var responseAsBody = await response.Content.ReadFromJsonAsync<ResponseModelDto<int>>();

            if (!response.IsSuccessStatusCode)
            {
                //loglama yapılacak
                responseAsBody!.FailMessages!.ForEach(x =>
                {
                    logger.LogError(x);
                });


                return "Sıcaklık Bilgisi Alınamadı";
            }

            return responseAsBody!.Data.ToString();

        }
    } 
}
