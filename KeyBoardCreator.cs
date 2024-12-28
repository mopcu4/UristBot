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
        public static InlineKeyboardMarkup[][] Back()
        {
            InlineKeyboardButton back = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[back]];
        }
        public static InlineKeyboardMarkup[] Auth()
        {
            InlineKeyboardButton auth = InlineKeyboardButton.WithCallbackData(text: "Авторизоваться", callbackData: "auth");
            return [auth];
        }
        public static InlineKeyboardButton[][] Courts()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("КС РФ", new WebAppInfo() { Url = "https://ksrf.ru/ru/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("ВС РФ", new WebAppInfo() { Url = "https://www.vsrf.ru/" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithWebApp("МС ХМАО", new WebAppInfo() { Url = "https://admhmao.ru/organy-vlasti/sudebnye-organy/mirovye-sudi/" });
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithWebApp("АС ХМАО", new WebAppInfo() { Url = "https://hmao.arbitr.ru/" });
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithWebApp("СГС", new WebAppInfo() { Url = "https://sudact.ru/regular/court/nDUwLklHgEWQ/?ysclid=m2vy6h0zm9400341220" });
            InlineKeyboardButton btn6 = InlineKeyboardButton.WithWebApp("СРС", new WebAppInfo() { Url = "https://sudact.ru/regular/court/KV28FiGb7S5r/?ysclid=m2vydpv45f811149352" });
            InlineKeyboardButton btn7 = InlineKeyboardButton.WithWebApp("ФАС", new WebAppInfo() { Url = "https://arbitr.ru/" });
            InlineKeyboardButton btn8 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2, btn3], [btn4, btn5, btn6], [btn7], [btn8]];
        }
        public static InlineKeyboardButton[][] Kodex()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithCallbackData(text: "ТК РФ", callbackData: "tk_rf");
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "УК РФ", callbackData: "yk_rf");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "ГК РФ", callbackData: "gk_rf");
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithCallbackData(text: "СК РФ", callbackData: "sk_rf");
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithCallbackData(text: "КоАП РФ", callbackData: "koap_rf");
            InlineKeyboardButton btn6 = InlineKeyboardButton.WithCallbackData(text: "УПК РФ", callbackData: "ypk_rf");
            InlineKeyboardButton btn7 = InlineKeyboardButton.WithCallbackData(text: "УИК РФ", callbackData: "yik_rf");
            InlineKeyboardButton btn8 = InlineKeyboardButton.WithCallbackData(text: "КАС РФ", callbackData: "kas_rf");
            InlineKeyboardButton btn9 = InlineKeyboardButton.WithCallbackData(text: "ГПК РФ", callbackData: "gpk_rf");
            InlineKeyboardButton btn10 = InlineKeyboardButton.WithCallbackData(text: "АПК РФ", callbackData: "apk_rf");
            InlineKeyboardButton btn11 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2, btn3, btn4], [btn5, btn6, btn7], [btn8, btn9, btn10], [btn11]];
        }
        public static InlineKeyboardButton[][] TkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/tk-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] YkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/uk-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] SkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/sk-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] GkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithCallbackData(text: "Перейти на сайт", callbackData: "sait_gk");
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] KoapRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/koap/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] YpkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/upk-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] YikRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/uik-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] GpkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/gpk-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] KasRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/kas-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] ApkRf()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Перейти на сайт", new WebAppInfo() { Url = "https://sudact.ru/law/apk-rf/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1], [btn2], [btn3]];
        }
        public static InlineKeyboardButton[][] SaitGk()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("1 часть ГК РФ", new WebAppInfo() { Url = "https://sudact.ru/law/gk-rf-chast1/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("2 часть ГК РФ", new WebAppInfo() { Url = "https://sudact.ru/law/gk-rf-chast2/" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithWebApp("3 часть ГК РФ", new WebAppInfo() { Url = "https://sudact.ru/law/gk-rf-chast3/" });
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithWebApp("4 часть ГК РФ", new WebAppInfo() { Url = "https://sudact.ru/law/gk-rf-chast4/" });
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "kodex");
            return [[btn1], [btn2], [btn3], [btn4], [btn5]];
        }
        public static InlineKeyboardButton[][] Fkz()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("26.02.1997", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-26021997-n-1-fkz/?ysclid=m2lpnty0vb369047848" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("21.07.1994", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-21071994-n-1-fkz/" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithWebApp("31.12.1996", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-31121996-n-1-fkz/" });
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithWebApp("07.02.2011", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-07022011-n-1-fkz/" });
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithWebApp("05.02.2014", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-05022014-n-3-fkz/" });
            InlineKeyboardButton btn6 = InlineKeyboardButton.WithWebApp("28.04.1995", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-konstitutsionnyi-zakon-ot-28041995-n-1-fkz/" });
            InlineKeyboardButton btn7 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2, btn3], [btn4, btn5, btn6], [btn7]];
        }
        public static InlineKeyboardButton[][] Fz()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithCallbackData(text: "3", callbackData: "fz_3");
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("7", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-10012002-n-7-fz-ob/?ysclid=m2lsgufor9913293217" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithWebApp("19", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-10012003-n-19-fz-o/?ysclid=m2lpipd89q799912597" });
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithWebApp("59", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-02052006-n-59-fz-o/?ysclid=m2lqymq88k424856899" });
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithWebApp("63", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-31052002-n-63-fz-ob/" });
            InlineKeyboardButton btn6 = InlineKeyboardButton.WithWebApp("79", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-27072004-n-79-fz-o/" });
            InlineKeyboardButton btn7 = InlineKeyboardButton.WithWebApp("109", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-18072006-n-109-fz-o/?ysclid=m2lp64rxaq280197248" });
            InlineKeyboardButton btn8 = InlineKeyboardButton.WithWebApp("114", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-15081996-n-114-fz-s/?ysclid=m2lpa4p4ch62950872" });
            InlineKeyboardButton btn9 = InlineKeyboardButton.WithWebApp("115", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-25072002-n-115-fz-o/?ysclid=m2lp4lkmu525647417" });
            InlineKeyboardButton btn10 = InlineKeyboardButton.WithWebApp("125", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-22102004-n-125-fz-ob/?ysclid=m2lpvue9rv427083474" });
            InlineKeyboardButton btn11 = InlineKeyboardButton.WithWebApp("144", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-12081995-n-144-fz-ob/?ysclid=m2lp1kka7g221333209" });
            InlineKeyboardButton btn12 = InlineKeyboardButton.WithWebApp("149", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-27072006-n-149-fz-ob/?ysclid=m2lpwt7dp1336443077" });
            InlineKeyboardButton btn13 = InlineKeyboardButton.WithWebApp("150", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-13121996-n-150-fz-ob/?ysclid=m2lot7d5mx744691023" });
            InlineKeyboardButton btn14 = InlineKeyboardButton.WithWebApp("152", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-27072006-n-152-fz-o/?ysclid=m2lq07n233370097640" });
            InlineKeyboardButton btn15 = InlineKeyboardButton.WithWebApp("2202-1", new WebAppInfo() { Url = "https://sudact.ru/law/zakon-rf-ot-17011992-n-2202-1-o/" });
            InlineKeyboardButton btn16 = InlineKeyboardButton.WithWebApp("3132", new WebAppInfo() { Url = "https://sudact.ru/law/zakon-rf-ot-26061992-n-3132-1-o/" });
            InlineKeyboardButton btn17 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2, btn3, btn4], [btn5, btn6, btn7, btn8], [btn9, btn10, btn11, btn12], [btn13, btn14, btn15, btn16], [btn17]];
        }
        public static InlineKeyboardButton[][] Fz_3()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("08.05.1994", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-08051994-n-3-fz-s/?ysclid=m2lpjytupz337929810" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("07.02.2011", new WebAppInfo() { Url = "https://sudact.ru/law/federalnyi-zakon-ot-07022011-n-3-fz-o/?ysclid=m2lp3cqh3h338800091" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "fz");
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2], [btn3], [btn4]];
        }
        public static InlineKeyboardButton[][] Orders()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("44", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-11042018-n-44-ob/?ysclid=m2ls73bf1o872269889" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("71", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-22052019-n-71-ob/?ysclid=m2lrn0o0uf247561452" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithWebApp("77", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-31072023-n-77-ob/?ysclid=m2ls268s5e641988735" });
            InlineKeyboardButton btn4 = InlineKeyboardButton.WithWebApp("170", new WebAppInfo() { Url = "https://www.consultant.ru/document/cons_doc_LAW_447240/?ysclid=m2lrzjp7qm369445330" });
            InlineKeyboardButton btn5 = InlineKeyboardButton.WithWebApp("216/689", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-miniusta-rossii-n-216-mvd-rossii/" });
            InlineKeyboardButton btn6 = InlineKeyboardButton.WithWebApp("236", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-20122019-n-236-ob/?ysclid=m2lrxjboi0564978562" });
            InlineKeyboardButton btn7 = InlineKeyboardButton.WithWebApp("237", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-rosarkhiva-ot-20122019-n-237-ob/?ysclid=m2lryyydfn474227770" });
            InlineKeyboardButton btn8 = InlineKeyboardButton.WithWebApp("615", new WebAppInfo() { Url = "https://sudact.ru/law/prikaz-mvd-rossii-ot-28072014-615-o/?ysclid=m2lrqb4pko795101697" });
            InlineKeyboardButton btn9 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2, btn3], [btn4, btn5, btn6], [btn7, btn8], [btn9]];
        }
        public static InlineKeyboardButton[][] Plenums()
        {
            InlineKeyboardButton btn1 = InlineKeyboardButton.WithWebApp("Пленум №5", new WebAppInfo() { Url = "https://sudact.ru/law/postanovlenie-plenuma-verkhovnogo-suda-rf-ot-24032005/" });
            InlineKeyboardButton btn2 = InlineKeyboardButton.WithWebApp("Пленум №29", new WebAppInfo() { Url = "https://sudact.ru/law/postanovlenie-plenuma-verkhovnogo-suda-rf-ot-27122002/" });
            InlineKeyboardButton btn3 = InlineKeyboardButton.WithCallbackData(text: "↩️Вернуться в главное меню", callbackData: "mainmenu");
            return [[btn1, btn2], [btn3]];
        }
    }
}