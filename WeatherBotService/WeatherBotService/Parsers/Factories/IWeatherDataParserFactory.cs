namespace WeatherBotService.Parsers;

public interface IWeatherDataParserFactory
{
    IWeatherDataParser GetParser(string inputData);
}