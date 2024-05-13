using FluentAssertions;
using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.Enums;
using WeatherBotStation.WeatherBots.Factories;
using WeatherBotStation.Configuration;

namespace WeatherBotStation.Test.WeatherBots;

public class WeatherBotFactoryTests
{
    private readonly WeatherBotFactory _factory = new();

    private readonly BotConfiguration _rainBotConfig = new()
    {
        Message = "It's raining",
        HumidityThreshold = 70.0
    };
    
    private readonly BotConfiguration _sunBotConfig = new()
    {
        Message = "It's sunny",
        TemperatureThreshold = 30.0
    };
    
    private readonly BotConfiguration _snowBotConfig = new()
    {
        Message = "It's snowing",
        TemperatureThreshold = 0.0
    };
    

    [Fact]
    public void CreateBot_WithRainBotConfiguration_ShouldReturnRainBot()
    {
        var bot = _factory.CreateBot(_rainBotConfig, WeatherBotType.RainBot);
        bot.Should().BeOfType<RainBot>();
    }

    [Fact]
    public void CreateBot_WithSunBotConfiguration_ShouldReturnSunBot()
    {
        var bot = _factory.CreateBot(_sunBotConfig, WeatherBotType.SunBot);
        bot.Should().BeOfType<SunBot>();
    }

    [Fact]
    public void CreateBot_WithSnowBotConfiguration_ShouldReturnSnowBot()
    {
        var bot = _factory.CreateBot(_snowBotConfig, WeatherBotType.SnowBot);
        bot.Should().BeOfType<SnowBot>();
    }

    [Fact]
    public void CreateBot_WithInvalidBotType_ShouldThrowArgumentException()
    {
        var action = () => _factory.CreateBot(_rainBotConfig, (WeatherBotType)10);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Invalid bot type");
    }
}