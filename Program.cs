using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

//TODO: set poll by date, set date, set up games poll, message count(чорт недели)
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

        // TODO: switch
        if (update.Message?.Text == "/start")
        {
            await client.SendMessage(update.Message.Chat.Id , "Хули выебываешься? \n/help вводи",
                replyParameters: update.Message?.MessageId);
        }
        else if (update.Message?.Text == "/help")
        {
            await client.SendMessage(update.Message.Chat.Id, "Команды\n/start\n"
                , replyParameters: update.Message?.MessageId);
        }

    }
}
