using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UrielBot;
using EntitiesItSpecBot;

namespace UristBot.UpdateHandlers
{
    public static class MessageHandler
    {
        private static ErrorLogger logger = new ErrorLogger();
        private static Dictionary<long, SielomUser> users = [];

        [Obsolete]
        public static async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var user = message?.From;
            var chat = message?.Chat;


            if (message?.Type == MessageType.Text)
            {
                Console.WriteLine($"{user?.FirstName} ({user?.Id}) написал(а) сообщение: {message?.Text}");


                if (message.Text == "/start")
                {
                    if (users.TryGetValue(message.From.Id, out var sielomUser))
                    {
                        InlineKeyboardMarkup replyMarkup = new(KeyBoardCreator.MainMenu());
                        await botClient.SendTextMessageAsync(chat.Id, "Необходимо авторизоваться!", replyMarkup: replyMarkup);
                        return;
                    }
                    else
                    {
                        InlineKeyboardMarkup firstmenu = InlineKeyboardButton.WithCallbackData(text: "Авторизоваться", callbackData: "auth");
                        await botClient.SendTextMessageAsync(chat.Id, "Необходимо авторизоваться!", replyMarkup: firstmenu);
                        return;
                    }
                }

                if (message.Text == "/dev")
                {
                    await botClient.SendTextMessageAsync(chat.Id,
                    "Разработчики:\n  Абдуллин Марсель Наилевич\n  Ананьева Анастасия Андреевна\n  Гераськин Сергей Александрович\n  Закуев Максим Александрович\n  Касаев Роман Викторович\n  Мукминов Сабир Робертович\n  Сорокин Вадим Иванович");
                    return;
                }

                if (user != null && BotState.UserLastButton.TryGetValue(user.Id, out var lastButton))
                {
                    string lastButtonValue;
                    if (lastButton == "tk_rf")
                    {
                        string[] ТК = { "https://sudact.ru/law/tk-rf/" };
                        string inputText = message.Text.Trim();
                        string searchText = inputText.Split(' ')[0];
                        lastButtonValue = await ParsingLogic.ParseWebsite(ТК, searchText, chat.Id, botClient, update, cancellationToken);
                          if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }

                    }
                    else if (lastButton == "yk_rf")
                    {
                        string[] УК = { "https://sudact.ru/law/uk-rf/" };
                        string inputText = message.Text.Trim();
                        string searchText = inputText.Split(' ')[0];
                         lastButtonValue = await ParsingLogic.ParseWebsite(УК, searchText, chat.Id, botClient, update, cancellationToken);
                           if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "sk_rf")
                    {
                        string[] СК = { "https://sudact.ru/law/sk-rf/" };

                        string inputText = message.Text.Trim();

                        string searchText = inputText.Split(' ')[0];
                        lastButtonValue = await ParsingLogic.ParseWebsite(СК, searchText, chat.Id, botClient, update, cancellationToken);
                          if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "gk_rf")
                    {
                        string[] ГК = {"https://sudact.ru/law/gk-rf-chast1/", "https://sudact.ru/law/gk-rf-chast2/",
                            "https://sudact.ru/law/gk-rf-chast3/", "https://sudact.ru/law/gk-rf-chast4/"};

                        string inputText = message.Text.Trim();

                        string searchText = inputText.Split(' ')[0];
                       lastButtonValue = await ParsingLogic.ParseWebsite(ГК, searchText, chat.Id, botClient, update, cancellationToken);
                          if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "koap_rf")
                    {
                        string[] КОАП = { "https://sudact.ru/law/koap/" };

                        string inputText = message.Text.Trim();

                        string searchText = inputText.Split(' ')[0];
                       lastButtonValue = await ParsingLogic.ParseWebsite(КОАП, searchText, chat.Id, botClient, update, cancellationToken);
                         if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "ypk_rf")
                    {
                        string[] УПК = { "https://sudact.ru/law/upk-rf/" };

                        string inputText = message.Text.Trim();

                        string searchText = inputText.Split(' ')[0];
                          lastButtonValue = await ParsingLogic.ParseWebsite(УПК, searchText, chat.Id, botClient, update, cancellationToken);
                          if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "yik_rf")
                    {
                        string[] УИК = { "https://sudact.ru/law/uik-rf/" };

                        string inputText = message.Text.Trim();

                        string searchText = inputText.Split(' ')[0];
                        lastButtonValue = await ParsingLogic.ParseWebsite(УИК, searchText, chat.Id, botClient, update, cancellationToken);
                           if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "gpk_rf")
                    {
                        string[] ГПК = { "https://sudact.ru/law/nk-rf-chast1/", "https://sudact.ru/law/nk-rf-chast2/" };

                        string inputText = message.Text.Trim();

                        string searchText = inputText.Split(' ')[0];
                         lastButtonValue = await ParsingLogic.ParseWebsite(ГПК, searchText, chat.Id, botClient, update, cancellationToken);
                           if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "kas_rf")
                    {
                        string[] КАС = { "https://sudact.ru/law/kas-rf/" };
                        string inputText = message.Text.Trim();
                        string searchText = inputText.Split(' ')[0];
                         lastButtonValue = await ParsingLogic.ParseWebsite(КАС, searchText, chat.Id, botClient, update, cancellationToken);
                           if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                    else if (lastButton == "apk_rf")
                    {
                        string[] АПК = { "https://sudact.ru/law/apk-rf/" };
                        string inputText = message.Text.Trim();
                        string searchText = inputText.Split(' ')[0];
                         lastButtonValue = await ParsingLogic.ParseWebsite(АПК, searchText, chat.Id, botClient, update, cancellationToken);
                           if (user != null)
                            {
                            BotState.UserLastButton[user.Id] = lastButtonValue;
                           }
                    }
                }
            }
            else if (message?.Type == MessageType.Contact)
            {
                var contact = message?.Contact;
                Console.WriteLine($"Пользователь {user?.FirstName} поделился информацией: Вот его номер:({contact?.PhoneNumber})");
                    try
                    {
                        InlineKeyboardMarkup keyboardMarkup = new(KeyBoardCreator.MainMenu());
                        _ = await botClient.SendTextMessageAsync(chat.Id, $"Чем я могу вам помочь?", replyMarkup: keyboardMarkup);
                    }
                    catch (Exception e) { logger.LogBotError("Ошибка при отправке сообщения пользователю", e); }
            }
        }
    }
}