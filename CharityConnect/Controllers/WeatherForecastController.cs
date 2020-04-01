using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityConnect.Backend.BusinessAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CharityConnect.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CharityConnect.Controllers
{
    [Authorize("Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserManager<AppUser> _user;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<AppUser> user)
        {
            _logger = logger;
            _user = user;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IEnumerable<WeatherForecast> Get()
        {
            var usertest = User.Identity.Name;
            var userGet = _user.FindByIdAsync(usertest).GetAwaiter().GetResult();

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
