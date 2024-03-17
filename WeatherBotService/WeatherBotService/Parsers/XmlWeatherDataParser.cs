using System.Xml.Serialization;
using WeatherBotService.Data;

namespace WeatherBotService.Parsers;

public class XmlWeatherDataParser : IWeatherDataParser
{
    public async Task<WeatherData?> Parse(string input)
    {
        return await Task.Run(() =>
        {
            var serializer = new XmlSerializer(typeof(WeatherData));
            using var reader = new StringReader(input);
            return (WeatherData)serializer.Deserialize(reader)!;
        });
    }
}