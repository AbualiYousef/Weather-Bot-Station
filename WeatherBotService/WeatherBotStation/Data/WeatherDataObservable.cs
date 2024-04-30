using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.BotManager;

namespace WeatherBotStation.Data;

public class WeatherDataObservable : IWeatherDataObservable
{
    private readonly IList<IWeatherBot> _bots = new List<IWeatherBot>();
    private WeatherData? _weatherData;

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
            _weatherData = data;
            if (data != null)
            {
                Notify();
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

    private void Notify()
    {
        foreach (var bot in _bots)
        {
            bot.Activate(_weatherData!);
        }
    }

    public IList<IWeatherBot> GetAllAttachedBots()
    {
        return _bots.AsReadOnly();
    }
}