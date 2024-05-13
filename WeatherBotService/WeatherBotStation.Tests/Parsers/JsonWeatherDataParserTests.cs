using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using WeatherBotStation.Data;
using WeatherBotStation.Parsers;

namespace WeatherBotStation.Test.Parsers;

public class JsonWeatherDataParserTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly JsonWeatherDataParser<WeatherData> _parser = new();

    [Fact]
    public async Task Parse_ValidInput_ShouldReturnWeatherData()
    {
        var weatherData = _fixture.Create<WeatherData>();

        var json = JsonConvert.SerializeObject(weatherData);

        var result = await _parser.ParseAsync(json);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(weatherData);
        result.Should().BeOfType<WeatherData>();
    }

    [Fact]
    public async Task Parse_InvalidInput_ShouldThrowsJsonReaderException()
    {
        var invalidJson = _fixture.Create<string>();

        var act = async () => { await _parser.ParseAsync(invalidJson); };

        await act.Should().ThrowAsync<JsonReaderException>();
    }

    [Fact]
    public async Task Parse_NullInput_ShouldThrowArgumentNullException()
    {
        string? nullJson = null;
        Func<Task> act = async () => { await _parser.ParseAsync(nullJson); };

        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'value')");    
    }
    
    [Fact]
    public async Task Parse_EmptyInput_ShouldReturnNull()
    {
        var result = await _parser.ParseAsync(string.Empty);

        result.Should().BeNull();
    }
    
    [Fact]
    public async Task Parse_JsonWithNullValues_ShouldHandleNulls()
    {
        var jsonWithNulls = "{\"Temperature\": null, \"Humidity\": null}";

        var result = await _parser.ParseAsync(jsonWithNulls);

        result.Should().NotBeNull();
        result.Temperature.Should().BeNull();
        result.Humidity.Should().BeNull();
    }
    
    [Fact]
    public async Task Parse_NegativeValues_ShouldReturnCorrectData()
    {
        var jsonWithNegativeValues = "{\"Temperature\": -5.5, \"Humidity\": -10.0}";

        var result = await _parser.ParseAsync(jsonWithNegativeValues);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(-5.5);
        result.Humidity.Should().Be(-10.0);
    }
    
    [Fact]
    public async Task Parse_NonNumericValues_ThrowsJsonReaderException()
    {
        var jsonWithStrings = "{\"Temperature\": \"hot\", \"Humidity\": \"high\"}";

        Func<Task> act = async () => { await _parser.ParseAsync(jsonWithStrings); };

        await act.Should().ThrowAsync<JsonReaderException>();
    }
    
    [Fact]
    public async Task Parse_JsonWithExtraProperties_ShouldIgnoreExtraProperties()
    {
        var jsonWithExtra = "{\"Temperature\": 20.0, \"Humidity\": 80.0, \"Pressure\": 1024}";

        var result = await _parser.ParseAsync(jsonWithExtra);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(20.0);
        result.Humidity.Should().Be(80.0);
    }
}