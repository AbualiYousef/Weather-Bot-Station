namespace WeatherBotStation.WeatherBots.BotManager;

public interface IWeatherBotManager
{
    IReadOnlyList<IWeatherBot> GetActivatedBots();
}