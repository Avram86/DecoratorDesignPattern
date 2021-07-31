using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class WeatherServiceLoggingDecorator : IWeatherService
    {
        private  IWeatherService _weatherService;
        private  ILogger<WeatherServiceLoggingDecorator> _logger;

        public WeatherServiceLoggingDecorator(IWeatherService weatherService, ILogger<WeatherServiceLoggingDecorator> logger)
        {
           _weatherService = weatherService;
            _logger = logger;
        }
        public CurrentWeather GetCurrentWeather(string location)
        {
            Stopwatch sw = Stopwatch.StartNew();
            CurrentWeather currentWeather = _weatherService.GetCurrentWeather(location);
            sw.Stop();

            long elipsedMillis = sw.ElapsedMilliseconds;

            _logger.LogWarning($"Retrieved weather data for {location}- Elipsed ms: {elipsedMillis} {@currentWeather}");

            return currentWeather;

            //OR
            //simple pass through to the class we want to decorate
           // return _weatherService.GetCurrentWeather(location);
        }

        public LocationForecast GetForecast(string location)
        {
            Stopwatch sw = Stopwatch.StartNew();
            LocationForecast forecast = _weatherService.GetForecast(location);
            sw.Stop();

            long elipsedMillis = sw.ElapsedMilliseconds;

            _logger.LogWarning($"Retrieved weather data for {location}- Elipsed ms: {elipsedMillis} {forecast}");

            return forecast;

            //OR
            //simple pass through to the class we want to decorate
            // return _weatherService.GetForecast(location);
        }

        //INFORM APPLICATION TO USE DECORATOR => HOMECONTROLLER
    }
}
