using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class WeatherServiceCachingDecorator : IWeatherService
    {
        private readonly IWeatherService _weatherService;
        private readonly IMemoryCache _cache;

        public WeatherServiceCachingDecorator(IWeatherService weatherService, IMemoryCache cache)
        {
            _weatherService = weatherService;
            _cache = cache;
        }
        public CurrentWeather GetCurrentWeather(string location)
        {
            string cacheKey = $"WeatherConditions::{location}";

            if(_cache.TryGetValue<CurrentWeather>(cacheKey, out var currentWeather))
            {
                //already have the value in the Cache
                return currentWeather;
            }
            else
            {
                //Save the data in the cache
                var currentConditions = _weatherService.GetCurrentWeather(location);

                _cache.Set<CurrentWeather>(cacheKey, currentConditions, TimeSpan.FromMinutes(30));

                return currentConditions;
            }
        }

        public LocationForecast GetForecast(string location)
        {
            string cacheKey = $"WeatherConditions::{location}";

            if (_cache.TryGetValue<LocationForecast>(cacheKey, out var forecast))
            {
                //already have the value in the Cache
                return forecast;
            }
            else
            {
                //Save the data in the cache
                var locationForecast = _weatherService.GetForecast(location);

                _cache.Set<LocationForecast>(cacheKey, locationForecast, TimeSpan.FromMinutes(30));

                return locationForecast;
            }
        }
    }
}
