using WeatherBotStation.WeatherBots;

namespace WeatherBotStation.Data;

public interface IWeatherDataObservable
{
    void Process(Task<WeatherData?> data);
    void Attach(WeatherBot bot);
    void Detach(WeatherBot bot);
}