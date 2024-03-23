namespace WeatherBotStation.Parsers;

public interface IWeatherDataParser<TInput>
{
    Task<TInput?> Parse(string input);
}