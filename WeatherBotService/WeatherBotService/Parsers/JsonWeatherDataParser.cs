using Newtonsoft.Json;
using WeatherBotService.Data;

namespace WeatherBotService.Parsers;

public class JsonWeatherDataParser : IWeatherDataParser
{
    public async Task<WeatherData?> Parse(string input)
    {
        return await Task.Run(() => JsonConvert.DeserializeObject<WeatherData>(input));
    }
}