using System.Xml.Serialization;
using AutoFixture;
using FluentAssertions;
using WeatherBotStation.Data;
using WeatherBotStation.Parsers;

namespace WeatherBotStation.Test.Parsers;

public class XmlWeatherDataParserTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly XmlWeatherDataParser<WeatherData> _parser = new XmlWeatherDataParser<WeatherData>();

    [Fact]
    public async Task Parse_ValidXmlInput_ShouldReturnWeatherData()
    {
        var weatherData = _fixture.Create<WeatherData>();
        var xml = SerializeToXml(weatherData);

        var result = await _parser.ParseAsync(xml);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(weatherData);
        result.Should().BeOfType<WeatherData>();
    }


    [Fact]
    public async Task Parse_EmptyXmlInput_ShouldThrowInvalidOperationException()
    {
        var emptyXml = string.Empty;

        var act = async () => { await _parser.ParseAsync(emptyXml); };

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("There is an error in XML document (0, 0).");
    }

    [Fact]
    public async Task Parse_XmlWithNullValues_ShouldHandleNulls()
    {
        var xmlWithNulls = @"<WeatherData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                            <Temperature xsi:nil=""true""/>
                            <Humidity xsi:nil=""true""/>
                         </WeatherData>";
        var result = await _parser.ParseAsync(xmlWithNulls);

        result.Should().NotBeNull();
        result.Temperature.Should().BeNull();
        result.Humidity.Should().BeNull();
    }

    [Fact]
    public async Task Parse_NegativeValuesInXml_ShouldReturnCorrectData()
    {
        var xmlWithNegativeValues =
            "<WeatherData><Temperature>-5.5</Temperature><Humidity>-10.0</Humidity></WeatherData>";

        var result = await _parser.ParseAsync(xmlWithNegativeValues);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(-5.5);
        result.Humidity.Should().Be(-10.0);
    }

    [Fact]
    public async Task Parse_XmlWithExtraProperties_ShouldIgnoreExtraProperties()
    {
        var xmlWithExtra =
            "<WeatherData><Temperature>20.0</Temperature><Humidity>80.0</Humidity><Pressure>1024</Pressure></WeatherData>";

        var result = await _parser.ParseAsync(xmlWithExtra);

        result.Should().NotBeNull();
        result.Temperature.Should().Be(20.0);
        result.Humidity.Should().Be(80.0);
    }

    private string SerializeToXml(WeatherData data)
    {
        using var stringWriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(WeatherData));
        serializer.Serialize(stringWriter, data);
        return stringWriter.ToString();
    }
}