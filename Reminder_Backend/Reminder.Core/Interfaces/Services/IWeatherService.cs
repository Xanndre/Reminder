using Reminder.Core.DTOs;
using System.Threading.Tasks;

namespace Reminder.Core.Interfaces.Services
{
    public interface IWeatherService
    {
        Task<WeatherDTO> GetCurrentWeather();
    }
}
