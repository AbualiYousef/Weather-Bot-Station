using WeatherBotStation.Data;

namespace WeatherBotStation.WeatherBots;


public class RainBot(string message, double humidityThreshold)
    : WeatherBot(message)
{
    protected override bool ShouldActivate(WeatherData weatherData) =>
        weatherData.Humidity is not null && weatherData.Humidity > humidityThreshold;
}