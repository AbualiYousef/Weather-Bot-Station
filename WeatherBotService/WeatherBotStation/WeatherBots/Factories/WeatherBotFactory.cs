using WeatherBotStation.Configuration;
using WeatherBotStation.WeatherBots.Enums;

namespace WeatherBotStation.WeatherBots.Factories;

public class WeatherBotFactory : IWeatherBotFactory
{
    public IWeatherBot CreateBot(BotConfiguration botConfiguration, WeatherBotType botType)
    {
        return botType switch
        {
            WeatherBotType.RainBot => new RainBot(botConfiguration.Message, botConfiguration.HumidityThreshold),
            WeatherBotType.SunBot => new SunBot(botConfiguration.Message,
                (double)botConfiguration.TemperatureThreshold!),
            WeatherBotType.SnowBot => new SnowBot(botConfiguration.Message,
                (double)botConfiguration.TemperatureThreshold!),
            _ => throw new ArgumentException("Invalid bot type")
        };
    }
}