namespace WeatherBotStation.Parsers;

public interface IWeatherDataParser<TInput>
{
    Task<TInput?> ParseAsync(string input);
}