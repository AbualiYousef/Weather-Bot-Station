using WeatherBotStation.WeatherBots;

namespace WeatherBotStation.Data;

public interface IWeatherDataObservable
{
    WeatherData WeatherData { get; set; }

    void Attach(WeatherBot bot);

    void Detach(WeatherBot bot);

    void Notify();
}