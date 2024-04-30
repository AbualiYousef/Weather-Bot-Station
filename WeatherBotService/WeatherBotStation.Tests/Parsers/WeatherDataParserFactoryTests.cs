using FluentAssertions;
using WeatherBotStation.Data;
using WeatherBotStation.Parsers;
using WeatherBotStation.Parsers.Factories;

namespace WeatherBotStation.Test.Parsers;

public class WeatherDataParserFactoryTests
{
    private readonly WeatherDataParserFactory _factory = new WeatherDataParserFactory();

    [Fact]
    public void GetParser_WithXmlInput_ShouldReturnXmlParser()
    {
        var xmlInput = "<WeatherData></WeatherData>";

        var parser = _factory.GetParser(xmlInput);

        parser.Should().BeOfType<XmlWeatherDataParser<WeatherData>>();
    }

    [Fact]
    public void GetParser_WithJsonInput_ShouldReturnJsonParser()
    {
        var jsonInput = "{\"WeatherData\":{}}";

        var parser = _factory.GetParser(jsonInput);

        parser.Should().BeOfType<JsonWeatherDataParser<WeatherData>>();
    }

    [Fact]
    public void GetParser_WithInvalidInput_ShouldThrowArgumentException()
    {
        var invalidInput = "WeatherData";

        Action act = () => _factory.GetParser(invalidInput);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid input format.");
    }

    [Fact]
    public void GetParser_WithNullInput_ShouldThrowArgumentException()
    {
        Action act = () => _factory.GetParser(null);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid input format.");
    }
}