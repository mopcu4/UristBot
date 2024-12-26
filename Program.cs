// dotnet new console -n TelegremBotDemo
// dotnet add package Telegram.Bot

//Тестовый бот запущен на @sielom_paralegal_bot

// Подключаем библиотеки
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using EntitiesItSpecBot;
namespace UristBot
{


    class Program //начало программы
    {
        private static ErrorLogger logger = new ErrorLogger();
        private static Dictionary<long, string> userLastButton = new(); // Словарь для хранения последней нажатой кнопки пользователя
        private static ITelegramBotClient? _botClient;
        private static ReceiverOptions? _receiverOptions;   // голова бота
        private static Dictionary<long, SielomUser> users = [];
        private static MongoConnector mgC = new MongoConnector();
        private static string token = "7972241050:AAG-28c_xLbDYKkYJKURT45nQlKA6d8PdRQ";//Main
        //private static string token = "6805925189:AAGFhEJmKt8CsfUPKMKvLLNWfHlSZJsxPHk";


        static async Task Main()
        {
            foreach (var user in await mgC.GetUsers())
            {
                users.TryAdd(user.TgId, user);
            }
            _botClient = new TelegramBotClient(token);   // что бы его получить заходим в @BotFather(пропишите /newbot, после name бота, а дальше и тег (@test_bot или че то подобное. Тег должен быть уникальным)
            _receiverOptions = new ReceiverOptions();

            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);   // _receiverOptions - инициализация(перемещение кода в телеграмм бот) cts.Token - завершение программы

            Console.WriteLine($"Бот запущен!");   // бот пишет в консольку имя и то что он запущен
            await Task.Delay(-1);
        }

        static async Task<Task> ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken) /*функция с ошибками(API ключа)*/
        {
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                => $"Telegram API Error:n[{apiRequestException.ErrorCode}]n{apiRequestException.Message}",
                _ => error.ToString()
            };
            logger.LogMessage(ErrorMessage);
            return Task.CompletedTask;
        }

        [Obsolete]
        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) // ОСНОВНАЯ ЧАСТЬ БОТА
        {
            try   // проверка на ошибки
            {
                switch (update.Type)   // коробка (обновление)
                {
                    case UpdateType.Message:   // киндер в этой коробке (сообщение). UpdateType.Message - тут мы прописываем самую 1-ую и основную кнопку, а так же /start
                        {

                            var message = update.Message;   // любое сообщение
                            var user = message?.From;   // пользователь
                            var chat = message?.Chat;   // id чата с пользователем (бот решает кому что писать)

                            switch (message?.Type)   // капсула с игрушкой (если пользователь что-то написал, то используется эта функция)
                            {
                                case MessageType.Text:   // игрушка (сообщение) в капсуле
                                    {
                                        Console.WriteLine($"{user?.FirstName} ({user?.Id}) написал(а) сообщение: {message?.Text}");   // Это выводит что пользователь вводит

                                        if (message.Text == "/start")    // Если мы в боте пишем /start, то функция работает, но если напишем что-либо другое, то бот ничего не ответит
                                        {
                                            if (users.TryGetValue(message.From.Id, out var sielomUser))
                                            {
                                                InlineKeyboardMarkup replyMarkup = new(KeyBoardCreator.MainMenu());
                                                await botClient.SendTextMessageAsync(chat.Id, "Необходимо авторизоваться!", replyMarkup: replyMarkup);
                                                return;
                                            }
                                            else
                                            {
                                                InlineKeyboardMarkup firstmenu = new(InlineKeyboardButton.WithCallbackData(text: "Авторизоваться", callbackData: "auth"));  
                                                await botClient.SendTextMessageAsync(chat.Id, "Необходимо авторизоваться!", replyMarkup: firstmenu);    
                                                return;
                                            }
                                        }

                                        if (!users.Keys.Contains(message.From.Id))
                                        {
                                            return;
                                        }
                                        if (message.Text == "/dev")
                                        {
                                            await botClient.SendTextMessageAsync(chat.Id,
                                            "Разработчики:\n  Абдуллин Марсель Наилевич\n  Ананьева Анастасия Андреевна\n  Гераськин Сергей Александрович\n  Закуев Максим Александрович\n  Касаев Роман Викторович\n  Мукминов Сабир Робертович\n  Сорокин Вадим Иванович");
                                            return;
                                        }

                                        if (userLastButton.TryGetValue(user.Id, out var lastButton))
                                        {
                                            if (lastButton == "tk_rf")
                                            {
                                                // Пример использования функции
                                                string[] ТК = { "https://sudact.ru/law/tk-rf/" };

                                                // Ввод текста для поиска
                                                string inputText = message.Text.Trim(); // Убираем лишние пробелы

                                                // Извлекаем номер статьи из введенного текста
                                                string searchText = inputText.Split(' ')[0]; // Берем первое слово как номер статьи

                                                // InlineKeyboardMarkup backs = new(new[]
                                                // {
                                                //     new []  {InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex")}
                                                // });

                                                await ParseWebsite(ТК, searchText, chat.Id, botClient, update, cancellationToken); // Исправлено: передаем массив строк
                                            }
                                            else if (lastButton == "yk_rf")
                                            {
                                                string[] УК = { "https://sudact.ru/law/uk-rf/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                // InlineKeyboardMarkup fkz = new(new[]
                                                // { new []  {InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex")}});

                                                await ParseWebsite(УК, searchText, chat.Id, botClient, update, cancellationToken);

                                            }
                                            else if (lastButton == "sk_rf")
                                            {
                                                string[] СК = { "https://sudact.ru/law/sk-rf/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(СК, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "gk_rf")
                                            {
                                                string[] ГК = {"https://sudact.ru/law/gk-rf-chast1/", "https://sudact.ru/law/gk-rf-chast2/",
                                            "https://sudact.ru/law/gk-rf-chast3/", "https://sudact.ru/law/gk-rf-chast4/"};

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(ГК, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "koap_rf")
                                            {
                                                string[] КОАП = { "https://sudact.ru/law/koap/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(КОАП, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "ypk_rf")
                                            {
                                                string[] УПК = { "https://sudact.ru/law/upk-rf/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(УПК, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "yik_rf")
                                            {
                                                string[] УИК = { "https://sudact.ru/law/uik-rf/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(УИК, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "gpk_rf")
                                            {
                                                string[] ГПК = { "https://sudact.ru/law/nk-rf-chast1/", "https://sudact.ru/law/nk-rf-chast2/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(ГПК, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "kas_rf")
                                            {
                                                string[] КАС = { "https://sudact.ru/law/kas-rf/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(КАС, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                            else if (lastButton == "apk_rf")
                                            {
                                                string[] АПК = { "https://sudact.ru/law/apk-rf/" };

                                                string inputText = message.Text.Trim();

                                                string searchText = inputText.Split(' ')[0];

                                                await ParseWebsite(АПК, searchText, chat.Id, botClient, update, cancellationToken);
                                            }
                                        }
                                        return;
                                    }
                                case MessageType.Contact: //Кейс который отвечает за наличие контакта
                                    {
                                        var contact = message?.Contact;
                                        Console.WriteLine($"Пользователь {user?.FirstName} поделился информацией: Вот его номер:({contact?.PhoneNumber})");
                                        string? data = await SielomRoutes.CheckStudent(contact.PhoneNumber);
                                        if (data is null) data = await SielomRoutes.CheckTeacher(contact.PhoneNumber);
                                        if (data is null) return;
                                        SielomUser? newUser = SielomUser.FromJson(data);
                                        if (newUser is null) return;
                                        newUser.TgId = message.From.Id;
                                        users.TryAdd(message.From.Id, newUser);
                                        await mgC.AddUserAsync(newUser);
                                        try
                                        {
                                            InlineKeyboardMarkup keyboardMarkup = new(KeyBoardCreator.MainMenu());
                                            _ = await botClient.SendTextMessageAsync(chat.Id, $"Чем я могу вам помочь?", replyMarkup: keyboardMarkup);
                                        }
                                        catch (Exception e)
                                        {
                                        }
                                    }
                                    return;
                            }

                        }
                        return;


                    case UpdateType.CallbackQuery:    // Самое интересное что тут есть. Любая возвращённая переменная 
                        {
                            if (!users.Keys.Contains(update.CallbackQuery.From.Id) && update.CallbackQuery.Data != "auth")
                            {
                                return;
                            }
                            var callbackQuery = update.CallbackQuery;    // возращающаяся переменная, которая используется для callbackData, без неё код c 157 по 238 строки работать не будет
                            var message = callbackQuery?.Message;   // любое сообщение от пользователя
                            var user = callbackQuery?.From;   // сам пользователь
                            var chat = callbackQuery?.Message?.Chat;   // id чата с пользователем (бот решает кому что писать)
                            var contact = message?.Contact;
                            userLastButton[user.Id] = callbackQuery.Data;

                            InlineKeyboardMarkup back = new(
                            new []  {InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")}); /*Это кнопка которая вернёт тебя назад в обычное меню */

                            InlineKeyboardMarkup courts = new(new[]
                            {new[]
                            { InlineKeyboardButton.WithWebApp("КС РФ", new WebAppInfo() {Url = "https://ksrf.ru/ru/"}),
                              InlineKeyboardButton.WithWebApp("ВС РФ", new WebAppInfo() {Url = "https://www.vsrf.ru/"}),
                              InlineKeyboardButton.WithWebApp(text: "МС ХМАО", new WebAppInfo() {Url = "https://admhmao.ru/organy-vlasti/sudebnye-organy/mirovye-sudi/"})
                            },
                        new []
                            { InlineKeyboardButton.WithWebApp("АС ХМАО", new WebAppInfo() {Url = "https://hmao.arbitr.ru/"}),
                              InlineKeyboardButton.WithWebApp(text: "СГС", new WebAppInfo() {Url = "https://sudact.ru/regular/court/nDUwLklHgEWQ/?ysclid=m2vy6h0zm9400341220"}),
                              InlineKeyboardButton.WithWebApp(text: "СРС", new WebAppInfo() {Url = "https://sudact.ru/regular/court/KV28FiGb7S5r/?ysclid=m2vydpv45f811149352"})
                              },
                        new []
                            { InlineKeyboardButton.WithWebApp("ФАС", new WebAppInfo() {Url = "https://arbitr.ru/"})},
                        new []
                            { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")}
                        });

                            InlineKeyboardMarkup kodex = new(new[]
                            {new[]
                            { InlineKeyboardButton.WithCallbackData(text: "ТК РФ",callbackData: "tk_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "УК РФ",callbackData: "yk_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "ГК РФ",callbackData: "gk_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "СК РФ",callbackData: "sk_rf")
                            },
                        new[]
                            { InlineKeyboardButton.WithCallbackData(text: "КоАП РФ",callbackData: "koap_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "УПК РФ",callbackData: "ypk_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "УИК РФ",callbackData: "yik_rf")
                              },
                        new[]
                            { InlineKeyboardButton.WithCallbackData(text: "КАС РФ",callbackData: "kas_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "ГПК РФ",callbackData: "gpk_rf"),
                              InlineKeyboardButton.WithCallbackData(text: "АПК РФ",callbackData: "apk_rf")},
                        new[]{InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню",callbackData: "mainmenu")}
                        });

                            InlineKeyboardMarkup tk_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/tk-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup yk_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/uk-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup sk_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/sk-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup gk_rf = new(new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Перейти на сайт", callbackData: "sait_gk") }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup koap_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/koap/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup ypk_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/upk-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup yik_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/uik-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup gpk_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/gpk-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup kas_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/kas-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });
                            InlineKeyboardMarkup apk_rf = new(new[] { new[] { InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/apk-rf/" }) }, new[] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex") }, new[] { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu") } });

                            InlineKeyboardMarkup sait_gk = new(new[]{ new[] {InlineKeyboardButton.WithWebApp("1 часть ГК РФ", new WebAppInfo() {Url = "https://sudact.ru/law/gk-rf-chast1/"})},
                        new[] {InlineKeyboardButton.WithWebApp("2 часть ГК РФ", new WebAppInfo() {Url = "https://sudact.ru/law/gk-rf-chast2/"})},
                        new[] {InlineKeyboardButton.WithWebApp("3 часть ГК РФ", new WebAppInfo() {Url = "https://sudact.ru/law/gk-rf-chast3/"})},
                        new[] {InlineKeyboardButton.WithWebApp("4 часть ГК РФ", new WebAppInfo() {Url = "https://sudact.ru/law/gk-rf-chast4/"})},
                        new[] {InlineKeyboardButton.WithCallbackData(text: "Назад",callbackData: "kodex")}
                        });

                            InlineKeyboardMarkup fkz = new(new[]
                            { new[]
                            { InlineKeyboardButton.WithWebApp("26.02.1997", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-26021997-n-1-fkz/?ysclid=m2lpnty0vb369047848"}),
                              InlineKeyboardButton.WithWebApp("21.07.1994", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-21071994-n-1-fkz/"}),
                              InlineKeyboardButton.WithWebApp("31.12.1996", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-31121996-n-1-fkz/"})
                            },
                            new[]
                            { InlineKeyboardButton.WithWebApp("07.02.2011", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-07022011-n-1-fkz/"}),
                              InlineKeyboardButton.WithWebApp("05.02.2014", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-05022014-n-3-fkz/"}),
                              InlineKeyboardButton.WithWebApp("28.04.1995", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-28041995-n-1-fkz/"})
                            },
                            new[]{InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню",callbackData: "mainmenu")}
                        });


                            InlineKeyboardMarkup fz = new(new[]
                            { new[]
                            { InlineKeyboardButton.WithCallbackData(text: "3", callbackData: "fz_3"),
                              InlineKeyboardButton.WithWebApp("7", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-10012002-n-7-fz-ob/?ysclid=m2lsgufor9913293217"}),
                              InlineKeyboardButton.WithWebApp("19", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-10012003-n-19-fz-o/?ysclid=m2lpipd89q799912597"}),
                              InlineKeyboardButton.WithWebApp("59", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-02052006-n-59-fz-o/?ysclid=m2lqymq88k424856899"})

                            },
                          new[]
                            {
                              InlineKeyboardButton.WithWebApp("63", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-31052002-n-63-fz-ob/"}),
                              InlineKeyboardButton.WithWebApp("79", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-27072004-n-79-fz-o/"}),
                              InlineKeyboardButton.WithWebApp("109", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-18072006-n-109-fz-o/?ysclid=m2lp64rxaq280197248"}),
                              InlineKeyboardButton.WithWebApp("114", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-15081996-n-114-fz-s/?ysclid=m2lpa4p4ch62950872"})

                            },
                          new[]
                            {
                              InlineKeyboardButton.WithWebApp("115", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-25072002-n-115-fz-o/?ysclid=m2lp4lkmu525647417"}),
                              InlineKeyboardButton.WithWebApp("125", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-22102004-n-125-fz-ob/?ysclid=m2lpvue9rv427083474"}),
                              InlineKeyboardButton.WithWebApp("144", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-12081995-n-144-fz-ob/?ysclid=m2lp1kka7g221333209"}),
                              InlineKeyboardButton.WithWebApp("149", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-27072006-n-149-fz-ob/?ysclid=m2lpwt7dp1336443077"})

                            },
                          new[]
                            {
                              InlineKeyboardButton.WithWebApp("150", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-13121996-n-150-fz-ob/?ysclid=m2lot7d5mx744691023"}),
                              InlineKeyboardButton.WithWebApp("152", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-27072006-n-152-fz-o/?ysclid=m2lq07n233370097640"}),
                              InlineKeyboardButton.WithWebApp("2202-1", new WebAppInfo() {Url = "https://sudact.ru/law/zakon-rf-ot-17011992-n-2202-1-o/"}),
                              InlineKeyboardButton.WithWebApp("3132", new WebAppInfo() {Url = "https://sudact.ru/law/zakon-rf-ot-26061992-n-3132-1-o/"})
                            },
                          new[]{ InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню",callbackData: "mainmenu")}
                        });

                            InlineKeyboardMarkup orders = new(new[]
                                { new[]
                            { InlineKeyboardButton.WithWebApp("44", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-11042018-n-44-ob/?ysclid=m2ls73bf1o872269889"}),
                              InlineKeyboardButton.WithWebApp("71", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-22052019-n-71-ob/?ysclid=m2lrn0o0uf247561452"}),
                              InlineKeyboardButton.WithWebApp("77", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-31072023-n-77-ob/?ysclid=m2ls268s5e641988735"})
                            },
                            new[]
                            { InlineKeyboardButton.WithWebApp("170", new WebAppInfo() {Url = "https://www.consultant.ru/document/cons_doc_LAW_447240/?ysclid=m2lrzjp7qm369445330"}),
                              InlineKeyboardButton.WithWebApp("216/689", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-miniusta-rossii-n-216-mvd-rossii/"}),
                              InlineKeyboardButton.WithWebApp("236", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-20122019-n-236-ob/?ysclid=m2lrxjboi0564978562"})
                            },
                            new[]
                            { InlineKeyboardButton.WithWebApp("237", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-20122019-n-237-ob/?ysclid=m2lryyydfn474227770"}),
                              InlineKeyboardButton.WithWebApp("615", new WebAppInfo() {Url = "https://sudact.ru/law/prikaz-mvd-rossii-ot-28072014-615-o/?ysclid=m2lrqb4pko795101697"})
                            },
                            new[]{ InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню",callbackData: "mainmenu")}
                        });

                            InlineKeyboardMarkup plenums = new(new[]
                            {new[]
                            { InlineKeyboardButton.WithWebApp("Пленум №5", new WebAppInfo() {Url = "https://sudact.ru/law/postanovlenie-plenuma-verkhovnogo-suda-rf-ot-24032005/"}),
                              InlineKeyboardButton.WithWebApp("Пленум №29", new WebAppInfo() {Url = "https://sudact.ru/law/postanovlenie-plenuma-verkhovnogo-suda-rf-ot-27122002/"})},
                        new []
                            { InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")}
                        });


                            // эта функция нужна для консоли, чтобы в консоли отображалось, что пользователь нажал на кнопку
                            if (message?.Text != null || user != null || chat != null)
                            {
                                Console.WriteLine($"{user?.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery?.Data}"); /*Если пользователь нажал кнопку то будет написанно на что он нажал и id пользователя*/
                            }

                            bool firstMessageReceived = false;

                            if (update.Message != null && !firstMessageReceived)
                            {
                                firstMessageReceived = true;
                            }
                            if (update.Message != null && update.Message.Text != null && firstMessageReceived)
                            {
                                await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, "Спасибо за ваше сообщение!", replyMarkup: back);
                            }

                            switch (callbackQuery?.Data) //это свитч который выводит сообщения по callback
                            {
                                
                                case "mainmenu":  //переменная, которая отправляет сообщение используя replyMarkup
                                    {
                                        InlineKeyboardMarkup replyMarkup = KeyBoardCreator.MainMenu();
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Чем я могу вам помочь?", replyMarkup: replyMarkup);
                                        return;

                                    };
                                case "kodex":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Кодексы'", replyMarkup: kodex);
                                        return;
                                    }
                                case "tk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: tk_rf);
                                        return;
                                    }
                                case "yk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: yk_rf);
                                        return;
                                    }
                                case "sk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: sk_rf);
                                        return;
                                    }
                                case "gk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: gk_rf);
                                        return;
                                    }
                                case "sait_gk":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Перейти на сайт'", replyMarkup: sait_gk);
                                        return;
                                    }
                                case "koap_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: koap_rf);
                                        return;
                                    }
                                case "ypk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: ypk_rf);
                                        return;
                                    }
                                case "yik_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: yik_rf);
                                        return;
                                    }
                                case "gpk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: gpk_rf);
                                        return;
                                    }
                                case "kas_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: kas_rf);
                                        return;
                                    }
                                case "apk_rf":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Введите номер статьи", replyMarkup: apk_rf);
                                        return;
                                    }
                                case "fkz":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'ФКЗ'", replyMarkup: fkz);
                                        return;
                                    }
                                case "fz":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'ФЗ'", replyMarkup: fz);
                                        return;
                                    }
                                case "fz_3":
                                    {
                                        InlineKeyboardMarkup fz_3 = new(new[]
                                        {new[]
                                        { InlineKeyboardButton.WithWebApp("08.05.1994", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-08051994-n-3-fz-s/?ysclid=m2lpjytupz337929810"}),
                                          InlineKeyboardButton.WithWebApp("07.02.2011", new WebAppInfo() {Url = "https://sudact.ru/law/federalnyi-zakon-ot-07022011-n-3-fz-o/?ysclid=m2lp3cqh3h338800091"})
                                        },
                                    new[] {InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "fz")}, new[] {InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")}});
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Выберите дату", replyMarkup: fz_3);
                                        return;
                                    }
                                case "courts":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладу 'Суды'", replyMarkup: courts);
                                        return;
                                    }
                                case "plenums":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Пленумы'", replyMarkup: plenums);
                                        return;
                                    }
                                case "orders":
                                    {
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, $"Вы перешли во вкладку 'Приказы'", replyMarkup: orders);
                                        return;
                                    }
                                case "auth": //работает ваще
                                    {
                                        await botClient.DeleteMessageAsync(chatId: message.Chat.Id, messageId: message.MessageId, cancellationToken: cancellationToken);
                                        ReplyKeyboardMarkup authorization = new(new[]
                                        {
                                    KeyboardButton.WithRequestContact("Поделиться контактом!")
                                    })
                                        { ResizeKeyboard = true, OneTimeKeyboard = true };

                                        await botClient.SendTextMessageAsync(chat.Id, $"Я не могу предоставить доступ к данным, пока не узнаю кто их просит.\nПожалуйста, поделитесь своим контактом для авторизации.\n\nКнопка для ввода сообщений снизу. Если у вас она не появилась, нажмите на кнопку 🎛", replyMarkup: authorization);
                                        break;
                                    }
                                case "exit": // выход из проги
                                    {
                                        InlineKeyboardMarkup firstmenu = new(new[]
                                        { new []  {InlineKeyboardButton.WithCallbackData(text: "Авторизоваться", callbackData: "auth")},});
                                        await botClient.EditMessageTextAsync(chat.Id, messageId: message.MessageId, "Необходимо авторизоваться!", replyMarkup: firstmenu);
                                        return;
                                    }
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private async static Task ParseWebsite(string[] url, string searchText, long chatid, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) // Убедитесь, что метод возвращает Task
        {
            var message = update.Message;   // любое сообщение
            var user = message?.From;   // пользователь
            var chat = chatid;   // id чата
            var browser = new ScrapingBrowser();//
            browser.Encoding = System.Text.Encoding.UTF8;

            //knopka

            List<WebPage> pages = new List<WebPage>();
            try
            {
                if (url.Length == 4)
                {
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[0]))));
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[1]))));
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[2]))));
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[3]))));
                }
                else if (url.Length == 3)
                {
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[0]))));
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[1]))));
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[2]))));
                }
                else if (url.Length == 2)
                {
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[0]))));
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[1]))));
                }
                else if (url.Length == 1)
                {
                    pages.Add(await Task.Run(() => browser.NavigateToPage(new Uri(url[0]))));
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Ошибка при доступе к {url}: {ex.Message}");
                return; // Выход из функции в случае ошибки
            }

            string output = ""; // Создаем строку для хранения вывода


            // Объявление переменных для хранения названий частей, разделов и глав
            string partName = ""; // Добавлено: объявление переменной partName
            string chastname = ""; // Добавлено: объявление переменной chastname
            string chastlink = ""; // Добавлено: объявление переменной chastlink
            string sectionName = ""; // Добавлено: объявление переменной sectionName
            string sectionLink = ""; // Добавлено: объявление переменной sectionLink
            string currentChapter = ""; // Добавлено: объявление переменной currentChapter
            string chapterLink = ""; // Добавлено: объявление переменной chapterLink
            var outputCount = 0;

            foreach (var page in pages)
            {
                var allElements = page.Html.CssSelect("[class^='m_level_']").ToList();

                // Извлекаем название части из HTML
                var partElement = page.Html.CssSelect("p.pCenter").FirstOrDefault(e => e.InnerText.Contains("ЧАСТЬ"));
                if (partElement != null)
                {
                    partName = partElement.InnerText.Trim(); // Сохраняем название части
                }

                foreach (var element in allElements)
                {
                    string innerText = element.InnerText.Trim(); // Оставляем оригинальный текст
                    string currentLink = ""; // Переменная для хранения ссылки
                                             // Находим дочерний элемент <a> и извлекаем ссылку
                    var linkElement = element.CssSelect("a").FirstOrDefault();

                    if (linkElement != null)
                    {
                        currentLink = linkElement.GetAttributeValue("href", string.Empty);

                    }
                    // Проверяем, является ли элемент частью
                    if (element.HasClass("m_level_2")) //часть
                    {
                        chastname = innerText;
                        var linkElementM2 = element.CssSelect("a").FirstOrDefault(); // Переименовано: находим ссылку в m_level_2
                        chastlink = linkElementM2 != null ? "https://sudact.ru" + linkElementM2.GetAttributeValue("href", string.Empty) : ""; // Ссылка на общую часть
                    }
                    if (element.HasClass("m_level_3")) //раздел и подраздел
                    {
                        sectionName = innerText; // Сохраняем название части
                        var linkElementM3 = element.CssSelect("a").FirstOrDefault(); // Переименовано: находим ссылку в m_level_3
                        sectionLink = linkElementM3 != null ? "https://sudact.ru" + linkElementM3.GetAttributeValue("href", string.Empty) : ""; // Ссылка на особую часть
                    }
                    if (element.HasClass("m_level_4")) //статья
                    {
                        currentChapter = innerText; // Сохраняем название главы
                        var linkElementChapter = element.CssSelect("a").FirstOrDefault();
                        chapterLink = linkElementChapter != null ? "https://sudact.ru" + linkElementChapter.GetAttributeValue("href", string.Empty) : ""; // Исправлено: присваиваем значение chapterLink
                    }

                    // Проверяем, соответствует ли текст введенному номеру статьи
                    if (innerText.Contains($"Статья {searchText}.", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(chastname)) // Проверка на наличие названия части
                        {
                            output += $"{chastname.Split(' ')[0]}: {chastname} (ссылка: {chastlink})\n"; // Выводим название части

                            if (sectionName.Split(' ')[0] == "Раздел" ^ sectionName.Split(' ')[0] == "Подраздел" ^ sectionName.Split(' ')[0] == "Глава") // Проверка на наличие названия раздела
                            {
                                output += $"{sectionName.Split(' ')[0]}: {sectionName} (ссылка: {sectionLink})\n";
                            }
                            if (currentChapter.Split(' ')[0] == "Глава" ^ currentChapter.Split(' ')[0] == "§") // Проверка на наличие названия главы 
                            {
                                output += $"{currentChapter.Split(' ')[0]}: {currentChapter} (ссылка: {chapterLink})\n";
                            }
                            if (innerText.Split(' ')[0] == "Глава" ^ innerText.Split(' ')[0] == "§") // Проверка на наличие названия главы 
                            {
                                output += $"{innerText.Split(' ')[0]}: {innerText} (ссылка: {currentLink})\n";
                            }
                            output += $"Статья: {innerText} (ссылка: https://sudact.ru{currentLink})\n\n"; // Выводим название статьи
                        }
                    }

                }

                // Проверяем, достигли ли мы двух выводов


                // Выводим все собранные данные

                if (!string.IsNullOrEmpty(output) && outputCount == 0 && userLastButton.TryGetValue(user.Id, out var lastButton))
                {
                    try
                    {
                        await botClient.DeleteMessageAsync(chatId: message.Chat.Id, messageId: message.MessageId - 1, cancellationToken: cancellationToken);
                    }
                    catch (Exception e)
                    {
                        await botClient.DeleteMessageAsync(chatId: message.Chat.Id, messageId: message.MessageId, cancellationToken: cancellationToken);
                    }
                    InlineKeyboardMarkup back = new(new[]
                    {
                new[]{InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")},
                new []  {InlineKeyboardButton.WithCallbackData(text: "Сделать запрос ещё раз", callbackData: $"{lastButton}")},
                new []  {InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex")}
            });



                    outputCount += 1;
                    await botClient.SendTextMessageAsync(chat, output, cancellationToken: cancellationToken, replyMarkup: back);
                    userLastButton[user.Id] = "****************";
                }
                else
                {
                    InlineKeyboardMarkup back1 = new(new[]
                   {
                new[]{InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")},
                new []  {InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex")}
            });
                    await botClient.SendTextMessageAsync(chat, "Статья не найдена", cancellationToken: cancellationToken, replyMarkup: back1);
                }
            }
        }
    }
}