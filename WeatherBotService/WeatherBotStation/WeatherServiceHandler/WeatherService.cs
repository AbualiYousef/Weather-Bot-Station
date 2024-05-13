using WeatherBotStation.Data;
using WeatherBotStation.Parsers.Factories;
using WeatherBotStation.Utilities;

namespace WeatherBotStation.WeatherServiceHandler;

public class WeatherService(
    IWeatherDataObservable weatherDataObservable,
    IWeatherDataParserFactory weatherDataParserFactory)
    : IWeatherService
{
    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine(StandardMessages.EnterWeatherData);

            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            await ProcessInput(input);
        }
    }

    private async Task ProcessInput(string input)
    {
        try
        {
            var parser = weatherDataParserFactory.GetParser(input);
            var weatherData = await parser.Parse(input);
            weatherDataObservable.Process(weatherData);
        }
        catch (Exception e)
        {
            Console.WriteLine(StandardMessages.GenerateParsingErrorMessage(e.Message));
        }
    }
}