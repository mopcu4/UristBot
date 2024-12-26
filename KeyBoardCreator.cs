using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace UristBot
{
    public static class KeyBoardCreator
    {
        public static InlineKeyboardButton[][] MainMenu()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp(text: "🕛Конституция РФ", new WebAppInfo() { Url = "https://sudact.ru/law/konstitutsiia/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "📓Кодексы", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "📚ФКЗ", callbackData: "fkz");
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithCallbackData(text: "🧑‍⚖️Суды", callbackData: "courts");
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithCallbackData(text: "📖ФЗ", callbackData: "fz");
            InlineKeyboardButton btn6 = InlineKeyboardButton.WithCallbackData(text: "✅Приказы", callbackData: "orders");
            InlineKeyboardButton btn7 = InlineKeyboardButton.WithCallbackData(text: "📃Пленумы", callbackData: "plenums");
            InlineKeyboardButton btn8 = InlineKeyboardButton.WithWebApp("📈Росстат", new WebAppInfo() { Url = "https://rosstat.gov.ru/" });
            InlineKeyboardButton btn9 = InlineKeyboardButton.WithWebApp("👩‍💼Роструд", new WebAppInfo() { Url = "https://rostrud.gov.ru/" });
            InlineKeyboardButton btn10 = InlineKeyboardButton.WithCallbackData(text: "💔Выход", callbackData: "exit");
            return [[btn1], [btn2, btn3], [btn4, btn5], [btn6, btn7], [btn8, btn9], [btn10]];
        }
    }
}
