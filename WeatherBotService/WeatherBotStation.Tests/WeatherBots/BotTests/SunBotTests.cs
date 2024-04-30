using AutoFixture;
using WeatherBotStation.Data;
using WeatherBotStation.WeatherBots;
using FluentAssertions;

namespace WeatherBotStation.Test.WeatherBots.BotTests;

public class SunBotTests
{
    private readonly string _botMessage;
    private readonly double _temperatureThreshold;
    private readonly StringWriter _consoleOutput;
    private readonly SunBot _sunBot;

    public SunBotTests()
    {
        var fixture = new Fixture();
        _botMessage = fixture.Create<string>();
        _temperatureThreshold = 30; //from the appssettings.json configuration
        _sunBot = new SunBot(_botMessage, _temperatureThreshold);
        _consoleOutput = new StringWriter();
        Console.SetOut(_consoleOutput);
    }

    [Fact]
    public void Activate_WithTemperatureAboveThreshold_ShouldOutputActivationMessage()
    {
        var temperature = _temperatureThreshold + 5;
        var weatherData = new WeatherData { Temperature = temperature };
        _sunBot.Activate(weatherData);
        _consoleOutput.ToString().Should().Contain(_botMessage, "because the temperature is above the threshold.");
    }

    [Fact]
    public void Activate_WithTemperatureAtThreshold_ShouldNotOutputActivationMessage()
    {
        var weatherData = new WeatherData { Temperature = _temperatureThreshold };
        _sunBot.Activate(weatherData);
        _consoleOutput.ToString().Should().BeEmpty("because the temperature is not strictly above the threshold.");
    }

    [Theory]
    [InlineData(22.0)]  
    [InlineData(null)]
    public void Activate_LessThanOrEqualToThresholdOrNullTemperature_ShouldNotOutputActivationMessage(
        double? temperature)
    {
        _consoleOutput.GetStringBuilder().Clear();
        var weatherData = new WeatherData { Temperature = temperature };
        _sunBot.Activate(weatherData);
        _consoleOutput.ToString().Should().BeEmpty("because the temperature is not strictly above the threshold.");
    }
}