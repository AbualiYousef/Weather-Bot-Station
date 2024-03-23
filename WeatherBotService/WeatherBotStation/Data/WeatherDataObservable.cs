using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.BotManager;

namespace WeatherBotStation.Data;

public class WeatherDataObservable(IWeatherBotManager manager) : IWeatherDataObservable
{
    private readonly IList<IWeatherBot> _bots = manager.GetBots();
    private WeatherData _weatherData = null!;

    public WeatherData WeatherData
    {
        get => _weatherData;
        set
        {
            _weatherData = value;
            Notify();
        }
    }

    public void Attach(WeatherBot bot) => _bots.Add(bot);

    public void Detach(WeatherBot bot) => _bots.Remove(bot);

    public void Notify()
    {
        foreach (var bot in _bots)
        {
            bot.Activate(_weatherData);
        }
    }
}