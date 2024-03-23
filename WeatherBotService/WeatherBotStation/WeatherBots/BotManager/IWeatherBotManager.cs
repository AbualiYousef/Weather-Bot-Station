namespace WeatherBotStation.WeatherBots.BotManager;

public interface IWeatherBotManager
{
    IList<IWeatherBot> GetBots();
}