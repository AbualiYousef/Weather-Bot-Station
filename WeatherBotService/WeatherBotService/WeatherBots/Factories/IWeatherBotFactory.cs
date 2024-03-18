using WeatherBotService.Configuration;
using WeatherBotService.WeatherBots.Enums;

namespace WeatherBotService.WeatherBots.Factories;

public interface IWeatherBotFactory
{
    IWeatherBot CreateBot(WeatherBotType type, string message, BotConfiguration botConfiguration);
}