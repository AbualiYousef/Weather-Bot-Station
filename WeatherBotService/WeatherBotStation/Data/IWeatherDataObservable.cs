using WeatherBotStation.WeatherBots;

namespace WeatherBotStation.Data;

public interface IWeatherDataObservable
{
    void Process(WeatherData? data);
    void Attach(IWeatherBot bot);
    void Detach(IWeatherBot bot);
}