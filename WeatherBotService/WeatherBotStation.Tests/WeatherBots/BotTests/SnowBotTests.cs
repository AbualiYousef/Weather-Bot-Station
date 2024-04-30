using WeatherBotStation.Data;
using WeatherBotStation.WeatherBots;
using AutoFixture;
using FluentAssertions;

namespace WeatherBotStation.Test.WeatherBots.BotTests;

public class SnowBotTests
{
    private readonly string _botMessage;
    private readonly double _temperatureThreshold;
    private readonly StringWriter _consoleOutput;
    private readonly SnowBot _snowBot;

    public SnowBotTests()
    {
        var fixture = new Fixture();
        _botMessage = fixture.Create<string>();
        _temperatureThreshold = 0; //from the appssettings.json configuration
        _snowBot = new SnowBot(_botMessage, _temperatureThreshold);
        _consoleOutput = new StringWriter();
        Console.SetOut(_consoleOutput);
    }

    [Fact]
    public void Activate_LowerThanThreshold_ShouldOutputActivationMessage()
    {
        var weatherData = new WeatherData { Temperature = _temperatureThreshold - 10.0 };
        _snowBot.Activate(weatherData);
        _consoleOutput.ToString().Should().Contain(_botMessage, "because the temperature is below the threshold.");
    }

    [Fact]
    public void Activate_ExactlyAtThreshold_ShouldNotOutputActivationMessage()
    {
        var weatherData = new WeatherData { Temperature = _temperatureThreshold };
        _snowBot.Activate(weatherData);
        _consoleOutput.ToString().Should().BeEmpty("because the temperature is not strictly below the threshold.");
    }

    [Theory]
    [InlineData(10.0)]
    [InlineData(null)]
    public void Activate_GreaterThanOrEqualToThresholdOrNullTemperature_ShouldNotOutputActivationMessage(
        double? temperature)
    {
        var weatherData = new WeatherData { Temperature = temperature };
        _snowBot.Activate(weatherData);
        _consoleOutput.ToString().Should().BeEmpty("because the temperature is not below the threshold.");
    }
}