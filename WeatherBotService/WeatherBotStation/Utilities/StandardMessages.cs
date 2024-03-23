namespace WeatherBotStation.Utilities;

public static class StandardMessages
{
    public const string InvalidConfigurationFile = "Invalid configuration file.";

    public const string EnterWeatherData = "Enter weather data (JSON or XML): ";

    public const string InvalidInput = "Invalid input format.";

    public static string GenerateBotActivationMessage(string botName, string message) =>
        $"""
         {botName} Activated!
         {botName}: {message}
         """;

    public static string GenerateParsingErrorMessage(string errorMessage) =>
        $"""
         An error has occurred while parsing input:
         {errorMessage}
         Please fix the error and try again.
         """;
    
}