using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.IService;
using Project.IService.Interface;

namespace Project.Core.Main.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        //readonly IAdvertisementService _advertisementServices;
        public ISys_DictionaryService service;
        //private readonly ILogger<WeatherForecastController> _logger;
        //ILogger<WeatherForecastController> logger,

        public WeatherForecastController(ISys_DictionaryService _service)
        {
            service = _service;
            //_logger = logger;
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
    }
}
