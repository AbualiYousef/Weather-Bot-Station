using WeatherBotStation.Data;

namespace WeatherBotStation.WeatherBots;

public interface IWeatherBot
{
    void Activate(WeatherData weatherData);
}