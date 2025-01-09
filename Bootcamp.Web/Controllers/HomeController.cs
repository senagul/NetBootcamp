using Bootcamp.Web.Models;
using Bootcamp.Web.WeatherServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bootcamp.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, WeatherService weatherService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            #region 1.yol
            //1.yol
            //var response = await weatherService.GetWeatherForecastWithCity("Ýstanbul");

            //if (response.IsSuccess)
            //{
            //    ViewBag.temp = response.Data;
            //}
            //else
            //{
            //    ViewBag.temp = "Sýcaklýk bilgisi alýnamadý";
            //} 
            #endregion


            ViewBag.temp = await weatherService.GetWeatherForecastWithCityBetter("Ýstanbul");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
