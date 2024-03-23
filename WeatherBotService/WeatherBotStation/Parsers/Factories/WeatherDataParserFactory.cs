using WeatherBotStation.Data;
using WeatherBotStation.Utilities;

namespace WeatherBotStation.Parsers.Factories;

public class WeatherDataParserFactory: IWeatherDataParserFactory
{
    public IWeatherDataParser<WeatherData> GetParser(string inputData)
    {
          return inputData switch
         {
            string s when s.StartsWith("<") => new XmlWeatherDataParser<WeatherData>(),
            string s when s.StartsWith("{") => new JsonWeatherDataParser<WeatherData>(),
            _ => throw new ArgumentException(StandardMessages.InvalidInput)
        };
    }
}