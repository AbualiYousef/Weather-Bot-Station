namespace WeatherBotService.Parsers;

public class WeatherDataParserFactory
{
    public static IWeatherDataParser GetParser(string inputData)
    {
        return inputData switch
        {
            string s when s.StartsWith("<") => new XmlWeatherDataParser(),
            string s when s.StartsWith("{") => new JsonWeatherDataParser(),
            _ => throw new ArgumentException("Unknown input format")
        };
    }
}