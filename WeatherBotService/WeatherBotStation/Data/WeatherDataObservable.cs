using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.BotManager;

namespace WeatherBotStation.Data;

public class WeatherDataObservable : IWeatherDataObservable
{
    private readonly IList<IWeatherBot> _bots = new List<IWeatherBot>();

    public WeatherDataObservable(IWeatherBotManager manager)
    {
        foreach (var bot in manager.GetActivatedBots())
        {
            Attach(bot);
        }
    }

    public void Process(WeatherData? data)
    {
        try
        {
            if (data != null)
            {
                Notify(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing weather data: {ex.Message}");
        }
    }

    public void Attach(IWeatherBot bot)
    {
        if (!_bots.Contains(bot))
        {
            _bots.Add(bot);
        }
    }

    public void Detach(IWeatherBot bot)
    {
        _bots.Remove(bot);
    }

    private void Notify(WeatherData data)
    {
        foreach (var bot in _bots)
        {
            bot.Activate(data);
        }
    }

    public IList<IWeatherBot> GetAllAttachedBots()
    {
        return _bots.AsReadOnly();
    }
}