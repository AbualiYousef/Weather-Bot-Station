using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.BotManager;

namespace WeatherBotStation.Data;

public class WeatherDataObservable(IWeatherBotManager manager) : IWeatherDataObservable
{
    private readonly IList<IWeatherBot> _bots = manager.GetActivatedBots();
    private WeatherData _weatherData = null!;

    public void Process(Task<WeatherData?> data)
    {
        _weatherData = data.Result!;
        Notify();
    }

    public void Attach(WeatherBot bot)
    {
        _bots.Add(bot);
    }

    public void Detach(WeatherBot bot)
    {
        _bots.Remove(bot);
    }

    private void Notify()
    {
        foreach (var bot in _bots)
            bot.Activate(_weatherData);
    }
}