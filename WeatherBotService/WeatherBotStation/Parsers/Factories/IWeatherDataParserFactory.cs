using WeatherBotStation.Data;

namespace WeatherBotStation.Parsers.Factories;

public interface IWeatherDataParserFactory
{
    IWeatherDataParser<WeatherData> GetParser(string inputData);
}