﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmegaModel.Weather;
using OmegaService.Weather.Interface;

namespace OmegaService.Weather.Controllers
{
    [ApiController]
    [Route("api/Weather/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFakeWeatherGetter _fakeWeatherGetter;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFakeWeatherGetter fakeWeatherGetter)
        {
            _logger = logger;
            _fakeWeatherGetter = fakeWeatherGetter;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _fakeWeatherGetter.GetRandomWeatherForecasts();
        }
    }
}
