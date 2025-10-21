using shop.Core.Dto;
using shop.Core.ServiceInterface;
using System.Text.Json;

namespace shop.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "";
            string baseUrl = "https://dataservice.accuweather.com/forecasts/v1/daily/1day/";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync($"{127964}?apikey={accuApiKey}&details=true");
                //if (response.IsSuccessStatusCode)
                //{
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonSerializer.Deserialize<AccuLocationRootDto>(jsonResponse);
                    
                    dto.EndDate = weatherData.Headline.EndDate;
                    dto.Text = weatherData.Headline.Text;
                    dto.TempMetricValueUnit = weatherData.DailyForecasts[0].Temperature.Maximum.Value;
                //}
                //else
                //{
                    // Handle error response
                    //throw new Exception("Error fetching weather data from AccuWeather API");
                //}

                return dto;
            }
        }
    }
}