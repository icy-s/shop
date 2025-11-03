using System;

namespace shop.Core.Dto
{
    public class OpenWeatherResultDto
    {
        public string? CityName { get; set; }
        public string? Country { get; set; }
        public double? Temperature { get; set; }
        public double? FeelsLike { get; set; }
        public double? TempMin { get; set; }
        public double? TempMax { get; set; }
        public int? Pressure { get; set; }
        public int? Humidity { get; set; }
        public double? WindSpeed { get; set; }
        public string? WeatherMain { get; set; }
        public string? WeatherDescription { get; set; }
        public string? Icon { get; set; }
        public DateTimeOffset? Sunrise { get; set; }
        public DateTimeOffset? Sunset { get; set; }
    }
}