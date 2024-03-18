using WeatherBotService.Data;

namespace WeatherBotService.WeatherBots;

public class SunBot(string message, double temperatureThreshold)
    : WeatherBot(message)
{
    protected override bool ShouldActivate(WeatherData weatherData) =>
        weatherData.Temperature is not null && weatherData.Temperature > temperatureThreshold;
}