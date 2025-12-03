using Microsoft.AspNetCore.Mvc;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Models.Weather;

namespace shop.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;

        public OpenWeatherController
            (
                IWeatherForecastServices weatherForecastServices
            )
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OpenWeather()
        {
            OpenWeatherViewModel vm = new();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OpenWeather(OpenWeatherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                OpenWeatherResultDto dto = new()
                {
                    CityName = model.CityName
                };

                dto = await _weatherForecastServices.OpenWeatherResult(dto);

                model.CityName = dto.CityName;
                model.Country = dto.Country;
                model.Temperature = dto.Temperature;
                model.FeelsLike = dto.FeelsLike;
                model.TempMin = dto.TempMin;
                model.TempMax = dto.TempMax;
                model.Pressure = dto.Pressure;
                model.Humidity = dto.Humidity;
                model.WindSpeed = dto.WindSpeed;
                model.WeatherMain = dto.WeatherMain;
                model.WeatherDescription = dto.WeatherDescription;
                model.Icon = dto.Icon;
                model.Sunrise = dto.Sunrise;
                model.Sunset = dto.Sunset;
                model.HasResult = true;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to load weather information right now. Please try again later.");
                model.HasResult = false;
            }

            return View(model);
        }
    }
}
