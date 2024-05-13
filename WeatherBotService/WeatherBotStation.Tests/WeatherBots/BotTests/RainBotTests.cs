using WeatherBotStation.Data;
using WeatherBotStation.WeatherBots;
using AutoFixture;
using FluentAssertions;

namespace WeatherBotStation.Test.WeatherBots.BotTests;

public class RainBotTests
{
    private readonly string _botMessage;
    private readonly double _humidityThreshold;
    private readonly StringWriter _consoleOutput;
    private readonly RainBot _rainBot;

    public RainBotTests()
    {
        var fixture = new Fixture();
        _botMessage = fixture.Create<string>();
        _humidityThreshold = 70; //from the appssettings.json configuration
        _consoleOutput = new StringWriter();
        _rainBot = new RainBot(_botMessage, _humidityThreshold);
        Console.SetOut(_consoleOutput);
    }

    [Fact]
    public void Activate_GreaterThanThreshold_ShouldOutputActivationMessage()
    {
        var weatherData = new WeatherData { Humidity = _humidityThreshold + 10.0 };
        _rainBot.Activate(weatherData);
        _consoleOutput.ToString().Should().Contain(_botMessage, "because the humidity is above the threshold.");
    }

    [Fact]
    public void Activate_ExactlyAtThreshold_ShouldNotOutputActivationMessage()
    {
        var weatherData = new WeatherData { Humidity = _humidityThreshold };
        _rainBot.Activate(weatherData);
        _consoleOutput.ToString().Should().BeEmpty("because the humidity is not strictly above the threshold.");
    }
    [Theory]
    [InlineData(70.0)]
    [InlineData(null)]
    public void Activate_LessThanOrEqualToThresholdOrNullHumidity_ShouldNotOutputActivationMessage(double? humidity)
    {
        var weatherData = new WeatherData { Humidity = humidity };
        _rainBot.Activate(weatherData);
        _consoleOutput.ToString().Should().BeEmpty("because the humidity is not above the threshold.");
    }
}