using WeatherBotService.WeatherBots;
using WeatherBotService.WeatherBots.BotManager;

namespace WeatherBotService.Data;

public class WeatherDataObservable(IWeatherBotManager manager, WeatherData weatherData) : IWeatherDataObservable
{
    private readonly IList<IWeatherBot> _bots = manager.GetBots();

    public WeatherData WeatherData
    {
        get => weatherData;
        set
        {
            weatherData = value;
            Notify();
        }
    }

    public void Attach(WeatherBot bot) => _bots.Add(bot);

    public void Detach(WeatherBot bot) => _bots.Remove(bot);

    public void Notify()
    {
        foreach (var bot in _bots)
        {
            bot.Activate(weatherData);
        }
    }
}