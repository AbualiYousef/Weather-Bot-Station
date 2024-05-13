using Moq;
using WeatherBotStation.Data;
using WeatherBotStation.WeatherBots;
using WeatherBotStation.WeatherBots.BotManager;
using FluentAssertions;

namespace WeatherBotStation.Test;

public class WeatherDataObservableTests
{
    private readonly WeatherDataObservable _weatherDataObservable;
    private readonly Mock<IWeatherBot> _mockBot;

    public WeatherDataObservableTests()
    {
        _mockBot = new Mock<IWeatherBot>();
        Mock<IWeatherBotManager> mockManager = new();
        mockManager
            .Setup(m => m.GetActivatedBots())
            .Returns(new List<IWeatherBot> { _mockBot.Object });

        _weatherDataObservable = new WeatherDataObservable(mockManager.Object);
    }

    [Fact]
    public void Process_WithWeatherData_ShouldNotifyBots()
    {
        var weatherData = new WeatherData { Temperature = 20.0, Humidity = 50.0 };

        _weatherDataObservable.Process(weatherData);

        _mockBot.Verify(b => b.Activate(weatherData), Times.Once);
    }

    [Fact]
    public void Process_WithNullWeatherData_ShouldNotNotifyBots()
    {
        _weatherDataObservable.Process(null);

        _mockBot.Verify(b => b.Activate(null), Times.Never);
    }

    [Fact]
    public void Attach_WithBot_ShouldAddBotIfItIsNotAlreadyAttached()
    {
        var newBot = new Mock<IWeatherBot>();
        _weatherDataObservable.Attach(newBot.Object);
        var attachedBots = _weatherDataObservable.GetAllAttachedBots();
        attachedBots.Should().Contain(newBot.Object);
    }

    [Fact]
    public void Attach_WithBot_ShouldNotAddBotIfItIsAlreadyAttached()
    {
        //detach all bots
        _weatherDataObservable.GetAllAttachedBots().ToList().ForEach(bot => _weatherDataObservable.Detach(bot));
        var newBot = new Mock<IWeatherBot>();
        var botInstance = newBot.Object;
        _weatherDataObservable.Attach(botInstance);
        _weatherDataObservable.Attach(botInstance);
        var attachedBots = _weatherDataObservable.GetAllAttachedBots();
        attachedBots.Should().HaveCount(1, "the same bot instance should not be added more than once");
        attachedBots.Should().ContainSingle().Which.Should().Be(botInstance);
    }
    
    [Fact]
    public void Detach_WithBot_ShouldRemoveBot()
    {
        var bot = new Mock<IWeatherBot>().Object;
        _weatherDataObservable.Attach(bot);
        _weatherDataObservable.Detach(bot);
        var attachedBots = _weatherDataObservable.GetAllAttachedBots();
        attachedBots.Should().NotContain(bot);
    }
    
    [Fact]
    public void GetAllAttachedBots_ShouldReturnAllAttachedBots()
    {
        //detach all bots
        _weatherDataObservable.GetAllAttachedBots().ToList().ForEach(bot => _weatherDataObservable.Detach(bot));
        var bot1 = new Mock<IWeatherBot>().Object;
        var bot2 = new Mock<IWeatherBot>().Object;
        _weatherDataObservable.Attach(bot1);
        _weatherDataObservable.Attach(bot2);
        var attachedBots = _weatherDataObservable.GetAllAttachedBots();
        attachedBots.Should().HaveCount(2);
        attachedBots.Should().Contain(bot1);
        attachedBots.Should().Contain(bot2);
    }
}