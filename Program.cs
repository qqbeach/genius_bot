using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

internal class Program
{
    private static void Main()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        string apiKey = config["ApiSettings:ApiKey"];
        Host genius = new Host(apiKey);
        genius.Start();

        genius.OnMessage += OnMessage;

        Console.ReadLine();
    }

    private static async void OnMessage(ITelegramBotClient client, Update update)
    {
        //await client.SendPoll

        if (update.Message?.Text == "/start")
        {
            await client.SendMessage(update.Message?.Chat.Id , "test start",
                replyParameters: update.Message?.MessageId);
        }

    }
}
