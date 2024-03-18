using WeatherBotService.Configuration;
using WeatherBotService.WeatherBots.Enums;

namespace WeatherBotService.WeatherBots.Factories;

public class WeatherBotFactory : IWeatherBotFactory
{
    public IWeatherBot CreateBot(WeatherBotType botType, string message, BotConfiguration botConfiguration)
    {
        return botType switch
        {
            WeatherBotType.RainBot => new RainBot(message, (double)botConfiguration.HumidityThreshold!),
            WeatherBotType.SunBot => new SunBot(message, (double)botConfiguration.TemperatureThreshold!),
            WeatherBotType.SnowBot => new SnowBot(message, (double)botConfiguration.TemperatureThreshold!),
            _ => throw new ArgumentException("Unknown Weather Bot type.", nameof(botType))
        };
    }
}