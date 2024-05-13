using System.Xml.Serialization;
using WeatherBotStation.Data;

namespace WeatherBotStation.Parsers;

public class XmlWeatherDataParser<TInput> :IWeatherDataParser<TInput>
{
    public async Task<TInput?> ParseAsync(string input)
    {
        return await Task.Run(() =>
        {
            var serializer = new XmlSerializer(typeof(WeatherData));
            using var reader = new StringReader(input);
            return (TInput?)serializer.Deserialize(reader);
        });
    }
}