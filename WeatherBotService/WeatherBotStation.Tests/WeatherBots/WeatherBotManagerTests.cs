using FluentAssertions;
using WeatherBotStation.WeatherBots.Enums;
using WeatherBotStation.WeatherBots.Factories;
using WeatherBotStation.Configuration;
using WeatherBotStation.Utilities;
using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.BotManager;

namespace WeatherBotStation.Test.WeatherBots;

public class WeatherBotManagerTests
{
    private readonly IWeatherBotFactory _weatherBotFactory = new WeatherBotFactory();

    private readonly Dictionary<WeatherBotType, BotConfiguration> _botConfigurations = new()
    {
        {
            WeatherBotType.RainBot,
            new BotConfiguration
                { Enabled = true, Message = "It looks like it's about to pour down!", HumidityThreshold = 70.0 }
        },
        {
            WeatherBotType.SunBot,
            new BotConfiguration
                { Enabled = true, Message = "Wow, it's a scorcher out there!", TemperatureThreshold = 30.0 }
        },
        {
            WeatherBotType.SnowBot,
            new BotConfiguration { Enabled = false, Message = "Brrr, it's getting chilly!", TemperatureThreshold = 0.0 }
        }
    };

    [Fact]
    public void GetActivatedBots_ShouldReturnEnabledBots()
    {
        var manager = new WeatherBotManager(_botConfigurations, _weatherBotFactory);

        var activatedBots = manager.GetActivatedBots();

        activatedBots.Should().HaveCount(2);
        activatedBots.Should().Contain(bot => bot is RainBot);
        activatedBots.Should().Contain(bot => bot is SunBot);
        activatedBots.Should().NotContain(bot => bot is SnowBot);
    }

    [Fact]
    public void GetActivatedBots_WithInvalidConfiguration_ShouldThrowInvalidOperationException()
    {
        _botConfigurations.Add((WeatherBotType)10, new BotConfiguration { Enabled = null });

        var manager = new WeatherBotManager(_botConfigurations, _weatherBotFactory);

        Action act = () => manager.GetActivatedBots();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage(StandardMessages.InvalidConfigurationFile);
    }
}