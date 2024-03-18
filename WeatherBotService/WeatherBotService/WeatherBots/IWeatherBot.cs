using WeatherBotService.Data;

namespace WeatherBotService.WeatherBots;

public interface IWeatherBot
{
    void Activate(WeatherData weatherData);
}