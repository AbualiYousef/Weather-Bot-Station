namespace WeatherBotService.WeatherBots.BotManager;

public interface IWeatherBotManager
{
    IList<IWeatherBot> GetBots();
}