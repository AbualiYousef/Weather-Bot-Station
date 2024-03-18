using WeatherBotService.Configuration;
using WeatherBotService.Utilities;
using WeatherBotService.WeatherBots.Enums;
using WeatherBotService.WeatherBots.Factories;

namespace WeatherBotService.WeatherBots.BotManager;

public class WeatherBotManager(
    IDictionary<WeatherBotType, BotConfiguration> botConfigurations,
    IWeatherBotFactory weatherBotFactory)
    : IWeatherBotManager
{
    public IList<IWeatherBot> GetBots()
    {
        var bots = new List<IWeatherBot>();
        foreach (var botConfiguration in botConfigurations)
        {
            if (botConfiguration.Value.Enabled is null)
                throw new InvalidOperationException(StandardMessages.InvalidConfigurationFile);
            if (!(bool)botConfiguration.Value.Enabled!)
                continue;
            var bot = weatherBotFactory
                .CreateBot(botConfiguration.Key, botConfiguration.Value.Message!, botConfiguration.Value);
            bots.Add(bot);
        }

        return bots;
    }
}