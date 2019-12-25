using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.IServices.Interface;

namespace Project.Core.Main.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        readonly IAdvertisementService _advertisementServices;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAdvertisementService advertisementServices)
        {
            _advertisementServices = advertisementServices;
            _logger = logger;
        }
        /// <summary>
        /// api 接口注释
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="a">参数1</param>
        /// <param name="b">参数2</param>
        /// <returns></returns>
        [HttpPost]
        public object Sum(int a ,int b)
        {
            return Ok(_advertisementServices.Sum(a,b));
        }
    }
}
