using shop.Core.Dto;

namespace shop.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<AccuLocationWeatherResultDto> AccuWeatherResultWebClient(AccuLocationWeatherResultDto dto);
    }
}
