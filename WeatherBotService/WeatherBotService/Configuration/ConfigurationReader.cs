using WeatherBotService.Data;
using WeatherBotService.Parsers;
using WeatherBotService.Utilities;

namespace WeatherBotService.Configuration;

public class ConfigurationReader(IWeatherDataParser weatherDataParser)
    : IConfigurationReader
{
    public async Task<WeatherData?> Read(string filePath)
    {
        var fileContent = await File.ReadAllTextAsync(filePath);
        var parsedData = await weatherDataParser.Parse(fileContent) ??
                         throw new Exception(StandardMessages.InvalidConfigurationFile);
        return parsedData;
    }
}