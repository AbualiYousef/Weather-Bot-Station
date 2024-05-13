using WeatherBotStation.Configuration;
using WeatherBotStation.Utilities;
using WeatherBotStation.WeatherBots.Enums;
using WeatherBotStation.WeatherBots.Factories;

namespace WeatherBotStation.WeatherBots.BotManager;

public class WeatherBotManager(
    IDictionary<WeatherBotType, BotConfiguration> botConfigurations,
    IWeatherBotFactory weatherBotFactory)
    : IWeatherBotManager
{
    public IReadOnlyList<IWeatherBot> GetActivatedBots()
    {
        var bots = new List<IWeatherBot>();
        foreach (var botConfiguration in botConfigurations)
        {
            if (botConfiguration.Value.Enabled is null)
                throw new InvalidOperationException(StandardMessages.InvalidConfigurationFile);
            if (!(bool)botConfiguration.Value.Enabled!)
                continue;
            var bot = weatherBotFactory
                .CreateBot(botConfiguration.Value, botConfiguration.Key);
            bots.Add(bot);
        }

        return bots;
    }
}