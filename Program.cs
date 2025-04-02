using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        string apiKey = config["ApiSettings:ApiKey"];
        Host genius = new Host(apiKey);
    }
}
