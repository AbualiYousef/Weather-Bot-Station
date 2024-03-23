using WeatherBotStation.Data;
using WeatherBotStation.Parsers.Factories;
using WeatherBotStation.Utilities;

namespace WeatherBotStation.WeatherServiceHandler;

public class WeatherService(IWeatherDataObservable weatherDataObservable
    ,  IWeatherDataParserFactory weatherDataParserFactory)
    : IWeatherService
{
    public Task Run()
    {
        while (true)
        {
            Console.WriteLine(StandardMessages.EnterWeatherData);

            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            ProcessInput(input);
        }
    }
    
    private void ProcessInput(string input)
    {
        try
        {
            var parser = weatherDataParserFactory.GetParser(input);
            var weatherData = parser.Parse(input);
            weatherDataObservable.WeatherData= weatherData.Result!;
        }
        catch (Exception e)
        {
            Console.WriteLine(StandardMessages.GenerateParsingErrorMessage(e.Message));
        }
    }
}