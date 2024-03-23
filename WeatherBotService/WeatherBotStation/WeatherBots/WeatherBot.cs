using WeatherBotStation.Data;
using WeatherBotStation.Utilities;

namespace WeatherBotStation.WeatherBots;

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