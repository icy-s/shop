using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace shop.Core.Dto
{
    public class OpenWeatherRootDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("weather")]
        public List<OpenWeatherWeatherDto>? Weather { get; set; }

        [JsonPropertyName("main")]
        public OpenWeatherMainDto? Main { get; set; }

        [JsonPropertyName("wind")]
        public OpenWeatherWindDto? Wind { get; set; }

        [JsonPropertyName("sys")]
        public OpenWeatherSysDto? Sys { get; set; }
    }

    public class OpenWeatherWeatherDto
    {
        [JsonPropertyName("main")]
        public string? Main { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
    }

    public class OpenWeatherMainDto
    {
        [JsonPropertyName("temp")]
        public double? Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double? FeelsLike { get; set; }

        [JsonPropertyName("temp_min")]
        public double? TempMin { get; set; }

        [JsonPropertyName("temp_max")]
        public double? TempMax { get; set; }

        [JsonPropertyName("pressure")]
        public int? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int? Humidity { get; set; }
    }

    public class OpenWeatherWindDto
    {
        [JsonPropertyName("speed")]
        public double? Speed { get; set; }
    }

    public class OpenWeatherSysDto
    {
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("sunrise")]
        public long? Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long? Sunset { get; set; }
    }
}