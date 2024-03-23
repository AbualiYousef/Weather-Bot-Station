using WeatherBotStation.WeatherBots.Enums;

namespace WeatherBotStation.Configuration;

public interface IConfigurationReader
{
    Task<IDictionary<WeatherBotType, BotConfiguration>> Read(string filePath);
}