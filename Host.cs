using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

class Host
{
   TelegramBotClient _botClient;

   public Host(string token)
   {
        _botClient = new TelegramBotClient(token);
   } 

    public void Start()
    {
        _botClient.StartReceiving(UpdateHandler, ErrorHandler);
    }


    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        Console.WriteLine(update.Message?.Text);
        await Task.CompletedTask;
    }
    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
        await Task.CompletedTask;
    }
}