using WeatherBotService.Data;

namespace WeatherBotService.Parsers;

public interface IWeatherDataParser
{
    Task<WeatherData?> Parse(string input);
}