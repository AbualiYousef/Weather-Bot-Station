using Newtonsoft.Json;

namespace WeatherBotStation.Parsers;
public class JsonWeatherDataParser<TInput> : IWeatherDataParser<TInput>
{
    public async Task<TInput?> ParseAsync(string input)
    {
        return (await Task.Run(() => JsonConvert.DeserializeObject<TInput>(input)))!;
    }
}