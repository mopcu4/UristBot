using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using UristBot.UpdateHandlers;
using EntitiesItSpecBot;

namespace UristBot
{
    class Program
    {
        private static ErrorLogger logger = new ErrorLogger();
        private static ITelegramBotClient? _botClient;
        private static ReceiverOptions? _receiverOptions;
        private static Dictionary<long, SielomUser> users = [];
        private static string token = "************************";

        static async Task Main()
        {
            _botClient = new TelegramBotClient(token);
            _receiverOptions = new ReceiverOptions();

            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

            Console.WriteLine($"Бот запущен!");
            await Task.Delay(-1);
        }
        static async Task<Task> ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:n[{apiRequestException.ErrorCode}]n{apiRequestException.Message}",
                _ => error.ToString()
            };
            logger.LogBotError(ErrorMessage);
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        [Obsolete]
        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case Telegram.Bot.Types.Enums.UpdateType.Message:
                        await MessageHandler.Handle(botClient, update, cancellationToken);
                        break;

                    case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                        await CallbackQueryHandler.Handle(botClient, update, cancellationToken);
                        break;
                }
            }
            catch (Exception e){logger.LogBotError("Ошибка телеграмм бота", e);}
        }
    }
}