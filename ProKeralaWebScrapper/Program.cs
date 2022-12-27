using HtmlAgilityPack;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CalendarWebScrapper
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            for (;;)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("0) Exit \n1) Export Panchang Data");
                Console.Write("Enter choice ---> ");
                string actionInput = Console.ReadLine();
                string[] months = DateTimeFormatInfo.CurrentInfo.MonthNames;
                Console.Write("Enter export file name (ex. data.xlsx) ---> ");
                string fileName = Console.ReadLine();
                Console.Write("Enter export file location (ex. C:\\Users\\{user}\\Desktop) ---> ");
                string fileLocaltion = Console.ReadLine();
                switch (actionInput)
                {
                    case "0":
                        Environment.Exit(0);
                        break;
                    case "1":
                        Console.Write("Enter Year ---> ");
                        string year = Console.ReadLine();
                        Console.WriteLine("Select Month");
                        for(int i = 0; i < months.Length -1; i++)
                        {
                            Console.WriteLine($"{i+1}) {months[i]}");
                        }
                        Console.Write("Month Number ---> ");
                        string month = Console.ReadLine();
                        int monthIndex = 0;
                        if (int.TryParse(month, out monthIndex) && !string.IsNullOrEmpty(year))
                        {
                            Console.WriteLine("Month name {0}", months[monthIndex - 1]);
                            Console.WriteLine("Exporting will start");
                            await ExportPanchangData(year, months[monthIndex - 1], monthIndex, fileName, fileLocaltion);
                        } else
                        {
                            Console.WriteLine("Please correct value");
                        }
                        break;

                    default:
                        Console.WriteLine("Please select right value");
                        break ;
                }

            }
            
        }

        public static async Task ExportPanchangData(string year,string month, int monthIndex, string fileName, string fileLocation)
        {
            List<FlatDay> list = new List<FlatDay>();

            var days = DateTime.DaysInMonth(int.Parse(year), monthIndex);

            for (int i = 0; i < days; i++)
            {
                string day = i < 9 ? $"0{i + 1}" : $"{i + 1}";
                string date = $"{year}-{month.ToLower()}-{day}";
                Console.WriteLine($"Getting data for {date}");
                var url = $"https://www.prokerala.com/astrology/telugu-panchangam/{date}.html?loc=4930956";
                HttpClient client = new HttpClient();
                var response = client.GetStringAsync(url).Result;
                var faltDay = parseHtml(response);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"flatten data for {date}");
                list.Add(faltDay);
                Console.WriteLine($"added data to list");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Waiting next day");
                Thread.Sleep(5000);

            }

            await SaveExecelFile(list, fileName, fileLocation);
        }

        public static async Task SaveExecelFile(List<FlatDay> flatDays, string fileName, string fileLocation)
        {
            var file = new FileInfo($@"{fileLocation}\{fileName}");

            DeleteIfExits(file);

            using var package = new ExcelPackage(file);
            var ws = package.Workbook.Worksheets.Add("Panchang Data");
            var range = ws.Cells["A1"].LoadFromCollection(flatDays, true);
            range.AutoFitColumns();
            await package.SaveAsync();
        }

        private static void DeleteIfExits(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public static FlatDay parseHtml(string htmlStr)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);

            var panchangDataDay = doc.DocumentNode.Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("panchang-data-day"));
            var dayList = panchangDataDay?.Descendants("li");
            Day day = new Day();
            if (dayList!= null)
            {
                foreach (var li in dayList)
                {
                    var spans = li.Descendants("span").ToList();
                    switch(spans[0].InnerText)
                    {
                        case "Vikram Samvat":
                            day.vikramSamvat = spans[1].InnerText;
                            break;
                        case "Indian Civil Calendar":
                            day.indianCivilCalendar = spans[1].InnerText;
                            break;
                        case "Purnimanta Month":
                            day.purnimantaMonth = spans[1].InnerText;
                            break;
                        case "Amanta Month":
                            day.amantaMonth = spans[1].InnerText;
                            break;
                        default:
                            Console.Write($"{spans[0].InnerText} not available in case");
                            break;
                    }
                }
            }

            day.tithi = GetKeyValuePairs("panchang-data-tithi", doc);
            day.nakshatra = GetKeyValuePairs("panchang-data-nakshatra", doc);
            day.karana = GetKeyValuePairs("panchang-data-karana", doc);
            day.yoga = GetKeyValuePairs("panchang-data-yoga", doc);
            day.sunAndMoonTiming = GetKeyValuePairs("panchang-data-sun_moon_timing", doc);
            day.inauspiciousPeriod = GetKeyValuePairs("panchang-data-inauspicious-period", doc);
            day.auspiciousPeriod = GetKeyValuePairs("panchang-data-auspicious-period", doc);
            day.anandadiYoga = GetKeyValuePairs("panchang-data-anandadi-yoga", doc);
            day.lunarMonth = GetKeyValuePairs("panchang-data-lunar-month", doc);
            day.others = GetKeyValuePairs("panchang-data-others", doc);
            day.vara = getValue("panchang-data-vaasara", doc);
            day.sooryaRasi = getValue("panchang-data-soorya-rasi", doc);
            day.chandraRasi = getValue("panchang-data-chandra-rasi", doc);
            day.tamilYoga = getValue("panchang-data-tamil-yoga", doc);
            day.chandrashtama = getValue("panchang-data-chandrashtama", doc);
            return flattenDay(day);
        }

        public static FlatDay flattenDay(Day day)
        {
            FlatDay flatDay = new FlatDay();
            
            foreach(var prop in day.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    flatDay.GetType().GetProperty(prop.Name).SetValue(flatDay, prop.GetValue(day));
                } else
                {
                    var value = prop.GetValue(day);
                    if (value != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        var totalCount = ((Dictionary<string, string>)value).Count;
                        int i = 1;
                        foreach (var v in (Dictionary<string, string>)value)
                        {
                            string str = i == totalCount ? $"{v.Key} - {v.Value} " : $"{v.Key} - {v.Value} \n";
                            sb.AppendLine(str);
                            i++;
                        }
                        flatDay.GetType().GetProperty(prop.Name).SetValue(flatDay, sb.ToString());
                    }
                }
            }

            return flatDay;
        }

        public static string getValue(string nodeClassName, HtmlDocument doc)
        {
            var formhtml = doc.DocumentNode.Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "").Contains(nodeClassName));
            var list = formhtml?.Descendants("li");
            string strValue = "";
            if (list != null)
            {
                foreach (var li in list)
                {
                    var spans = li.Descendants("span").ToList();
                    strValue = spans[0].InnerText;
                }
            }
            return strValue;
        }

        public static Dictionary<string, string> GetKeyValuePairs(string nodeClassName, HtmlDocument doc) 
        {
            var formhtml = doc.DocumentNode.Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "").Contains(nodeClassName));
            var list = formhtml?.Descendants("li");
            var dic = new Dictionary<string, string>();
            if (list != null)
            {
                foreach (var li in list)
                {
                    var spans = li.Descendants("span").ToList();
                    var value = "";
                    if (spans.Count > 1) {
                        value = spans[1].InnerText.Replace("&ndash;", "-");
                    }
                    dic.Add(spans[0].InnerText.Replace("&nbsp;", ""), value);
                }
            }
            return dic;
        }
    }
}
