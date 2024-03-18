using WeatherBotService.Data;

namespace WeatherBotService.Configuration;

public interface IConfigurationReader
{
    Task<WeatherData?> Read(string filePath);
}