using Microsoft.AspNetCore.DataProtection.KeyManagement;
using server.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace server.Tools
{
    public class CurrencyRates
    {
        public static readonly Dictionary<string, string> CurrencyNames = new Dictionary<string, string>
        {
            { "AED", "Дирхам ОАЭ" },
            { "AFN", "Афгани" },
            { "ALL", "Албанский лек" },
            { "AMD", "Армянский драм" },
            { "ANG", "Нидерландский антильский гульден" },
            { "AOA", "Кванза" },
            { "ARS", "Аргентинский песо" },
            { "AUD", "Австралийский доллар" },
            { "AWG", "Арубинский флорин" },
            { "AZN", "Азербайджанский манат" },
            { "BAM", "Конвертируемая марка" },
            { "BBD", "Барбадосский доллар" },
            { "BDT", "Така" },
            { "BGN", "Болгарский лев" },
            { "BHD", "Бахрейнский динар" },
            { "BIF", "Бурундийский франк" },
            { "BMD", "Бермудский доллар" },
            { "BND", "Брунейский доллар" },
            { "BOB", "Боливиано" },
            { "BRL", "Бразильский реал" },
            { "BSD", "Багамский доллар" },
            { "BTN", "Нгултрум" },
            { "BWP", "Пула" },
            { "BYN", "Белорусский рубль" },
            { "BZD", "Белизский доллар" },
            { "CAD", "Канадский доллар" },
            { "CDF", "Конголезский франк" },
            { "CHF", "Швейцарский франк" },
            { "CLP", "Чилийское песо" },
            { "CNY", "Китайский юань" },
            { "COP", "Колумбийский песо" },
            { "CRC", "Коста-риканский коллон" },
            { "CUC", "Кубинский конвертируемый песо" },
            { "CUP", "Кубинский песо" },
            { "CVS", "Эскудо Кабо-Верде" },
            { "CZK", "Чешская крона" },
            { "DJF", "Джибутийский франк" },
            { "DKK", "Датская крона" },
            { "DOP", "Доминиканский песо" },
            { "DZD", "Алжирский динар" },
            { "EGP", "Египетский фунт" },
            { "ERN", "Эритрейская накфа" },
            { "ESP", "Испанская песета" },
            { "ETB", "Эфиопский бир" },
            { "EUR", "Евро" },
            { "FJD", "Фиджийский доллар" },
            { "FKP", "Фолклендский фунт" },
            { "GBP", "Фунт стерлингов" },
            { "GEL", "Грузинский лари" },
            { "GGP", "Гернсийский фунт" },
            { "GHS", "Ганский седи" },
            { "GIP", "Гибралтарский фунт" },
            { "GMD", "Гамбийский даласи" },
            { "GNF", "Гвинейский франк" },
            { "GTQ", "Гватемальский кетсаль" },
            { "GYD", "Гайанский доллар" },
            { "HKD", "Гонконгский доллар" },
            { "HNL", "Гондурасская лемпира" },
            { "HRK", "Хорватская куна" },
            { "HTG", "Гаитянский гурд" },
            { "HUF", "Венгерский форинт" },
            { "IDR", "Индонезийская рупия" },
            { "ILS", "Израильский шекель" },
            { "IMP", "Остров Мэнский фунт" },
            { "INR", "Индийская рупия" },
            { "IQD", "Иракский динар" },
            { "IRR", "Иранский риал" },
            { "ISK", "Исландская крона" },
            { "JMD", "Ямайдский доллар" },
            { "JOD", "Иорданский динар" },
            { "JPY", "Японская иена" },
            { "KES", "Кенийский шиллинг" },
            { "KGS", "Кыргызский сом" },
            { "KHR", "Камбоджийский риель" },
            { "KMF", "Коморский франк" },
            { "KPW", "Северокорейская вона" },
            { "KRW", "Южнокорейская вона" },
            { "KWD", "Кувейтский динар" },
            { "KYD", "Каймановский доллар" },
            { "KZT", "Тенге" },
            { "LAK", "Лаосский кип" },
            { "LBP", "Ливанский фунт" },
            { "LKR", "Шри-ланкийская рупия" },
            { "LRD", "Либерийский доллар" },
            { "LSL", "Лоти" },
            { "LTL", "Литовский лит" },
            { "LVL", "Латвийский лат" },
            { "LYD", "Ливийский динар" },
            { "MAD", "Марокканский дирхам" },
            { "MDL", "Молдавский лей" },
            { "MGA", "Малагасийский ариари" },
            { "MKD", "Македонский денар" },
            { "MMK", "Мьянмский кьят" },
            { "MNT", "Монгольский тугрик" },
            { "MOP", "Патака" },
            { "MUR", "Маврикийская рупия" },
            { "MVR", "Мальдивская руфия" },
            { "MWK", "Квача" },
            { "MXN", "Мексиканское песо" },
            { "MYR", "Малайзийский рингит" },
            { "MZN", "Мозамбикский метикал" },
            { "NAD", "Намибийский доллар" },
            { "NGN", "Нигерийская наира" },
            { "NIO", "Никарагуанская кордоба" },
            { "NOK", "Норвежская крона" },
            { "NPR", "Непальская рупия" },
            { "NZD", "Новозеландский доллар" },
            { "OMR", "Оманский риал" },
            { "PAB", "Панамский бальбоа" },
            { "PEN", "Перуанский новый соль" },
            { "PGK", "Папуасийский кина" },
            { "PHP", "Филиппинское песо" },
            { "PKR", "Пакистанская рупия" },
            { "PLN", "Польский злотый" },
            { "PYG", "Парагвайский гуарани" },
            { "QAR", "Катарский риал" },
            { "RON", "Румынский лей" },
            { "RSD", "Сербский динар" },
            { "RUB", "Российский рубль" },
            { "RWF", "Руандийский франк" },
            { "SAR", "Саудовский риал" },
            { "SBD", "Соломоновы острова доллар" },
            { "SCR", "Сейшельская рупия" },
            { "SDG", "Суданский фунт" },
            { "SEK", "Шведская крона" },
            { "SGD", "Сингапурский доллар" },
            { "SHP", "Фунт Святой Елены" },
            { "SLL", "Леоне" },
            { "SOS", "Сомали" },
            { "SRD", "Суринамский доллар" },
            { "SSP", "Южносуданский фунт" },
            { "STN", "Добр Сан-Томе и Принсипи" },
            { "SYP", "Сирийский фунт" },
            { "SZL", "Свазилендский лилангени" },
            { "THB", "Тайский бат" },
            { "TJS", "Таджикский сомони" },
            { "TMT", "Туркменский манат" },
            { "TND", "Тунисский динар" },
            { "TOP", "Тонгана паанга" },
            { "TRY", "Турецкая лира" },
            { "TTD", "Тринидад и Тобаго доллар" },
            { "TWD", "Новый тайваньский доллар" },
            { "TZS", "Танзанийский шиллинг" },
            { "UAH", "Гривна" },
            { "UGX", "Угандийский шиллинг" },
            { "USD", "Доллар США" },
            { "UYU", "Уругвайский песо" },
            { "UZS", "Узбекский сум" },
            { "VES", "Венесуэльский боливар" },
            { "VND", "Вьетнамский донг" },
            { "VUV", "Вануатский вату" },
            { "WST", "Самоанский тала" },
            { "XAF", "Франк КФА" },
            { "XCD", "Восточнокарибский доллар" },
            { "XOF", "Франк КФА" },
            { "XPF", "Франк Тихоокеанского франка" },
            { "YER", "Йеменский риал" },
            { "ZAR", "Южноафриканский рэнд" },
            { "ZMW", "Замбийская квача" },
            { "ZWL", "Зимбабвийский доллар" }
        };
        public static readonly string RSSCenterBankOfRussia = "http://www.cbr.ru/scripts/XML_daily.asp";
        public static readonly string RSSCenterBankOfKazakhstan = "https://nationalbank.kz/rss/rates_all.xml";

        public static string GetCurrencyNameFromTag(string tag)
        {
            string name = string.Empty;

            if (tag != null)
            {
                if (CurrencyNames.ContainsKey(tag))
                {
                    name = CurrencyNames[tag];
                }
                else
                {
                    name = null;
                }
            }

            return name;
        }

        public static async Task<XDocument> GetXMLFromURL(string URL)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync(URL);
            await Task.Delay(500);
            var xmlDoc = XDocument.Parse(response);
            return xmlDoc;
        }

        public static async Task<List<CurrencyRate>> GetExchangeRatesFromCBK()
        {
            List<CurrencyRate> result = new List<CurrencyRate>();

            XDocument xml = await GetXMLFromURL(RSSCenterBankOfKazakhstan);
            foreach (var item in xml.Descendants("item"))
            {
                var title = item.Element("title")?.Value;  // Код валюты, например "USD"
                var description = item.Element("description")?.Value;  // Курс валюты

                if (title != null && description != null && title != "XDR")
                {

                    result.Add(new CurrencyRate()
                    {
                        CurrencyStringCode = title,
                        CurrencyName = GetCurrencyNameFromTag(title),
                        ExchangeRate = description
                    });
                }
            }

            return result;
        }

        public static async Task<List<CurrencyRate>> GetExchangeRatesFromCBR()
        {
            List<CurrencyRate> result = new List<CurrencyRate>();

            XDocument xml = await GetXMLFromURL(RSSCenterBankOfRussia);
            foreach (var valute in xml.Descendants("Valute"))
            {
                var charCode = valute.Element("CharCode")?.Value;  // Код валюты, например "USD"
                var value = valute.Element("Value")?.Value;  // Курс валюты
                var name = valute.Element("Name")?.Value;  // Название валюты

                if(charCode != "XDR")
                {
                    result.Add(new CurrencyRate()
                    {
                        CurrencyStringCode = charCode,
                        CurrencyName = GetCurrencyNameFromTag(charCode),
                        ExchangeRate = value
                    });
                }
            }

            return result;
        }
    }
}
