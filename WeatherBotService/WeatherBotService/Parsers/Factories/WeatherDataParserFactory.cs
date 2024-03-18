namespace WeatherBotService.Parsers.Factories;

public class WeatherDataParserFactory: IWeatherDataParserFactory
{
    public IWeatherDataParser GetParser(string inputData)
    {
        return inputData switch
        {
            string s when s.StartsWith("<") => new XmlWeatherDataParser(),
            string s when s.StartsWith("{") => new JsonWeatherDataParser(),
            _ => throw new ArgumentException("Unknown input format")
        };
    }
}