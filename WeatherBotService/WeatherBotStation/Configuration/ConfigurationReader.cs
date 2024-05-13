using WeatherBotStation.Parsers;
using WeatherBotStation.Utilities;
using WeatherBotStation.WeatherBots.Enums;

namespace WeatherBotStation.Configuration;

public class ConfigurationReader
    (IWeatherDataParser<Dictionary<WeatherBotType, BotConfiguration>>weatherDataParser)
    : IConfigurationReader
{
    public async Task<IDictionary<WeatherBotType, BotConfiguration>> Read(string filePath)
    {
        var fileContent = await File.ReadAllTextAsync(filePath);
        var parsedData = await weatherDataParser.ParseAsync(fileContent) ??
                         throw new Exception(StandardMessages.InvalidConfigurationFile);
        return parsedData;
    }
}