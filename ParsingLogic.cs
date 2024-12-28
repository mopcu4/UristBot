using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using EntitiesItSpecBot;
using UristBot;

namespace UrielBot
{
    public static class ParsingLogic
    {
        private static ErrorLogger logger = new ErrorLogger();

        public static async Task<string> ParseWebsite(string[] url, string searchText, long chatid, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;   // любое сообщение
            var user = message?.From;   // пользователь
            var chat = chatid;   // id чата
            var browser = new ScrapingBrowser();//
            browser.Encoding = System.Text.Encoding.UTF8;

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
            catch (WebException e) { logger.LogBotError($"Ошибка при доступе к {string.Join(", ", url)}", e); return null; }

            string output = ""; // Создаем строку для хранения вывода
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

                if (!string.IsNullOrEmpty(output) && outputCount == 0 && BotState.UserLastButton.TryGetValue(user.Id, out var lastButton))
                {
                    InlineKeyboardMarkup back = new(new[]
                    {
                        new[]{InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu")},
                        new[]{InlineKeyboardButton.WithCallbackData(text: "Сделать запрос ещё раз", callbackData: $"{lastButton}")},
                        new[]{InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex")}
                    });
                    outputCount += 1;
                    await botClient.SendTextMessageAsync(chat, output, cancellationToken: cancellationToken, replyMarkup: back);
                   
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
            if (user != null)
            {
                BotState.UserLastButton[user.Id] = "****************";
                return  BotState.UserLastButton[user.Id];
            }
            else
            {
                return "****************";
            }
           
        }
    }
}