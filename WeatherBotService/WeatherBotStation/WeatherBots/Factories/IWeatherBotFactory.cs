using WeatherBotStation.Configuration;
using WeatherBotStation.WeatherBots.Enums;

namespace WeatherBotStation.WeatherBots.Factories;

public interface IWeatherBotFactory
{
    IWeatherBot CreateBot(BotConfiguration botConfiguration, WeatherBotType botType);
}