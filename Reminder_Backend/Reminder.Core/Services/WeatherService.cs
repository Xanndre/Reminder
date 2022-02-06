using Microsoft.Extensions.Options;
using Reminder.Core.Config;
using Reminder.Core.DTOs;
using Reminder.Core.Interfaces.Services;
using Reminder.Core.Responses;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reminder.Core.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OpenWeatherOptions _openWeatherOptions;

        public WeatherService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherOptions> openWeatherOptions)
        {
            _httpClientFactory = httpClientFactory;
            _openWeatherOptions = openWeatherOptions.Value;
        }

        public async Task<WeatherDTO> GetCurrentWeather()
        {
            var client = _httpClientFactory.CreateClient();
            var city = _openWeatherOptions.City;
            var key = _openWeatherOptions.OpenWeatherMapKey;
            var url = _openWeatherOptions.Url;

            var response = await client.GetAsync($"{url}?q={city}&appid={key}");
            var content = await response.Content.ReadAsStreamAsync();

            var deserialized = await JsonSerializer.DeserializeAsync<OpenWeatherResponse>(content);

            return new WeatherDTO
            {
                Temperature = deserialized.main.temp - 273.15,
                Humidity = deserialized.main.humidity,
                Description = string.Join(", ", deserialized.weather.Select(w => w.description))
            };
        }
    }
}
