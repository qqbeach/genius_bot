using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
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

        // TODO: auto self pin
        var mes = update.Message?.Text?.ToLower();
        if (mes != null && mes.StartsWith("/poll"))
        {
            var day = mes.Substring(5);
            var weekDay = "";
            var desireDate = DateTime.Today;

            if (day.Contains("пят") || day.Contains("птн"))
            {
                weekDay = "Пятница";
                var daysToAdd = ((int)DayOfWeek.Friday - (int)DateTime.Today.DayOfWeek + 7) % 7;
                daysToAdd = daysToAdd == 0 ? 7 : daysToAdd;    
                desireDate = DateTime.Today.AddDays(daysToAdd);
            }
            else if (day.Contains("вск") || day.Contains("вос"))
            {
                weekDay = "Воскресенье";
                var daysToAdd = ((int)DayOfWeek.Sunday - (int)DateTime.Today.DayOfWeek + 7) % 7;
                daysToAdd = daysToAdd == 0 ? 7 : daysToAdd;    
                desireDate = DateTime.Today.AddDays(daysToAdd);  
            }
            else
            {
                weekDay = "Суббота";
                var daysToAdd = ((int)DayOfWeek.Saturday - (int)DateTime.Today.DayOfWeek + 7) % 7;
                daysToAdd = daysToAdd == 0 ? 7 : daysToAdd;    
                desireDate = DateTime.Today.AddDays(daysToAdd);
            }
            

            var send_poll = await client.SendPoll(update.Message.Chat.Id,
            $"Собираемся в {weekDay}, {desireDate.ToString("dd.MM.yyyy")}? Если да, то выбери час с которого ты будешь полностью свободен",
            [
                "У меня не выйдет",
                "Да, я свободен c 24(по гринвичу. Пиздец, у Дани час ночи будет, shame on me)",
                "Да, я свободен c 23(по гринвичу)",
                "Да, я свободен c 22(по гринвичу)",
                "Да, я свободен c 21(по гринвичу)",
                "Да, я свободен c 20(по гринвичу)",
                "Да, я свободен c 19(по гринвичу)",
                "Да, я свободен c 18(по гринвичу)",
                "Да, я свободен даже раньше"
            ], false, null, true);

            client?.PinChatMessage(update.Message.Chat.Id, send_poll.Id);
        }
        if (mes != null && mes.StartsWith("/games"))
        {
            string[] variants = mes.Split(' ');
            System.Console.WriteLine(variants);

            var send_game = await client.SendPoll(update?.Message?.Chat.Id,
            "В чё погамаем?",
            [
                    
            ]);
        }
        
        else if (update.Message?.Text == "/sayhi")
        {
            await client.DeleteMessage(update.Message.Chat.Id, update.Message.Id);
            await client.SendMessage(update.Message.Chat.Id, "Здарова черти ебаные");
        }
        //TODO: poll game.
        
        // else if (update.Message?.Text == "/start")
        // {
        //     await client.SendMessage(update.Message.Chat.Id , "Хули выебываешься? \n/help вводи",
        //         replyParameters: update.Message?.MessageId);
            
        // }
        // else if (update.Message?.Text == "/help")
        // {
        //     await client.SendMessage(update.Message.Chat.Id, "Команды\n/help\n/poll\n"
        //         , replyParameters: update.Message?.MessageId);
        // }

    }
}
