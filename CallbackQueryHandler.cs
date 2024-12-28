using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace UristBot.UpdateHandlers
{
    public static class CallbackQueryHandler
    {
        [Obsolete]
        public static async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var callbackQuery = update.CallbackQuery;
            var message = callbackQuery?.Message;
            var user = callbackQuery?.From;
            var chat = callbackQuery?.Message?.Chat;

            if (user != null)
            {
                BotState.UserLastButton[user.Id] = callbackQuery.Data;
            }

            if (message?.Text != null || user != null || chat != null)
            {
                Console.WriteLine($"{user?.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery?.Data}");
            }

            switch (callbackQuery?.Data)
            {
                case "mainmenu":
                    {
                        InlineKeyboardMarkup replyMarkup = new(KeyBoardCreator.MainMenu());
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Чем я могу вам помочь?", replyMarkup: replyMarkup);
                        return;
                    };
                case "kodex":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Кодексы'", replyMarkup: new(KeyBoardCreator.Kodex()));
                        return;
                    }
                case "tk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.TkRf()));
                        return;
                    }
                case "yk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.YkRf()));
                        return;
                    }
                case "sk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.SkRf()));
                        return;
                    }
                case "gk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.GkRf()));
                        return;
                    }
                case "sait_gk":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Перейти на сайт'", replyMarkup: new(KeyBoardCreator.SaitGk()));
                        return;
                    }
                case "koap_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.KoapRf()));
                        return;
                    }
                case "ypk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.YpkRf()));
                        return;
                    }
                case "yik_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.YikRf()));
                        return;
                    }
                case "gpk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.GpkRf()));
                        return;
                    }
                case "kas_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.KasRf()));
                        return;
                    }
                case "apk_rf":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: new(KeyBoardCreator.ApkRf()));
                        return;
                    }
                case "fkz":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'ФКЗ'", replyMarkup: new(KeyBoardCreator.Fkz()));
                        return;
                    }
                case "fz":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'ФЗ'", replyMarkup: new(KeyBoardCreator.Fz()));
                        return;
                    }
                case "fz_3":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Выберите дату", replyMarkup: new(KeyBoardCreator.Fz_3()));
                        return;
                    }
                case "courts":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладу 'Суды'", replyMarkup: new(KeyBoardCreator.Courts()));
                        return;
                    }
                case "plenums":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Пленумы'", replyMarkup: new(KeyBoardCreator.Plenums()));
                        return;
                    }
                case "orders":
                    {
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Приказы'", replyMarkup: new(KeyBoardCreator.Orders()));
                        return;
                    }
                case "auth":
                    {
                        await botClient.DeleteMessageAsync(chatId: message.Chat.Id, messageId: message.MessageId, cancellationToken: cancellationToken);
                        ReplyKeyboardMarkup authorization = new(new[] { KeyboardButton.WithRequestContact("Поделиться контактом!") }) { ResizeKeyboard = true, OneTimeKeyboard = true };
                        await botClient.SendTextMessageAsync(chat.Id, $"Я не могу предоставить доступ к данным, пока не узнаю кто их просит.\nПожалуйста, поделитесь своим контактом для авторизации.\n\nКнопка для ввода сообщений снизу. Если у вас она не появилась, нажмите на кнопку 🎛", replyMarkup: authorization);
                        break;
                    }
                case "exit":
                    {
                        InlineKeyboardMarkup firstmenu = InlineKeyboardButton.WithCallbackData(text: "Авторизоваться", callbackData: "auth");
                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, "Необходимо авторизоваться!", replyMarkup: firstmenu);
                        return;
                    }
            }
        }
    }
}