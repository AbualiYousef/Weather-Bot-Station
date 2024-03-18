using WeatherBotService.Data;
using WeatherBotService.Utilities;

namespace WeatherBotService.WeatherBots;

public abstract class WeatherBot(string message) : IWeatherBot
{
    public void Activate(WeatherData weatherData)
    {
        if (!ShouldActivate(weatherData)) return;
        var activationMessage = StandardMessages
            .GenerateBotActivationMessage(GetType().Name, message);
        Console.WriteLine(activationMessage);
    }

    protected abstract bool ShouldActivate(WeatherData weatherData);
}