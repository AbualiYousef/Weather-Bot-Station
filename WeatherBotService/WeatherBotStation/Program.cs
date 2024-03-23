using Microsoft.Extensions.DependencyInjection;
using WeatherBotStation.Configuration;
using WeatherBotStation.Data;
using WeatherBotStation.Parsers;
using WeatherBotStation.Parsers.Factories;
using WeatherBotStation.WeatherBots.BotManager;
using WeatherBotStation.WeatherBots.Enums;
using WeatherBotStation.WeatherBots.Factories;
using WeatherBotStation.WeatherServiceHandler;


var jsonParser = new JsonWeatherDataParser<Dictionary<WeatherBotType, BotConfiguration>>();
var configurationReader = new ConfigurationReader(jsonParser);
var configurations =
    await configurationReader.Read("appsettings.json");


//add services to the service collection
var serviceProvider = new ServiceCollection()
    .AddSingleton(configurations)
    .AddSingleton<IWeatherBotManager, WeatherBotManager>()
    .AddSingleton<IWeatherService, WeatherService>()
    .AddSingleton<IWeatherBotFactory, WeatherBotFactory>()
    .AddSingleton<IConfigurationReader>(configurationReader)
    .AddSingleton<IWeatherDataParserFactory,WeatherDataParserFactory>()
    .AddScoped<IWeatherDataObservable, WeatherDataObservable>()
    .AddScoped<IWeatherService, WeatherService>();

var app = serviceProvider
    .BuildServiceProvider()
    .GetRequiredService<IWeatherService>();
await app.Run();