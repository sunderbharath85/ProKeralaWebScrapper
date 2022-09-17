using OfficeOpenXml;
using System.Text;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ProKeralaWebScrapperApp
{
    public static class ExportHelper
    {
        public static async Task ExportPanchangData(
            string year, 
            string month, 
            int monthIndex, 
            string fileName, 
            string fileLocation, 
            Location location, 
            ProgressBar progressBar,
            Logger logger,
            int timeSpan)
        {
            List<FlatDay> list = new List<FlatDay>();

            var days = DateTime.DaysInMonth(int.Parse(year), monthIndex);
            
            for (int i = 0; i < days; i++)
            {
                try
                {
                    int dIndex = (i + 1);
                    string day = i < 9 ? $"0{dIndex}" : $"{dIndex}";
                    string date = $"{year}-{month.ToLower()}-{day}";
                    logger.info($"Getting data for {date}\n\r" + Environment.NewLine);
                    var url = $"https://www.prokerala.com/astrology/telugu-panchangam/{date}.html?loc={location.locationCode}";
                    HttpClient client = new HttpClient();
                    var response = client.GetStringAsync(url).Result;
                    var faltDay = parseHtml(response);
                    faltDay.gregorianDate = $"{day}-{month.ToLower()}-{year}";
                    Console.ForegroundColor = ConsoleColor.Green;
                    logger.info($"flatten data for {date}\n\r" + Environment.NewLine);
                    list.Add(faltDay);
                    logger.info($"added data to list" + Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                    logger.log($"Waiting for next day\n\r" + Environment.NewLine);
                    progressBar.Value = (dIndex * 100) / days; ;
                    Thread.Sleep(timeSpan * 1000);
                } catch (Exception ex)
                {
                    logger.error(ex.Message + "\n\r" + Environment.NewLine);
                }
            }

            await SaveExecelFile(list, fileName, fileLocation);
        }

        public static async Task SaveExecelFile(List<FlatDay> flatDays, string fileName, string fileLocation)
        {
            var file = new FileInfo($@"{fileLocation}\{fileName}");

            DeleteIfExits(file);

            var dt = Utility.ToDataTable(flatDays);
            using var package = new ExcelPackage(file);
            var ws = package.Workbook.Worksheets.Add("Panchang Data");
            var range = ws.Cells["A1"].LoadFromDataTable(dt, true);
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
            if (dayList != null)
            {
                foreach (var li in dayList)
                {
                    var spans = li.Descendants("span").ToList();
                    switch (spans[0].InnerText)
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

            foreach (var prop in day.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    flatDay.GetType().GetProperty(prop.Name).SetValue(flatDay, prop.GetValue(day));
                }
                else
                {
                    var value = prop.GetValue(day);
                    if (value != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        var totalCount = ((Dictionary<string, string>)value).Count;
                        int i = 1;
                        foreach (var v in (Dictionary<string, string>)value)
                        {
                            string str = i == totalCount ? $"{v.Key} - {v.Value} " : $"{v.Key} - {v.Value} | ";
                            sb.Append(str);
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
                    if (spans.Count > 1)
                    {
                        value = spans[1].InnerText.Replace("&ndash;", "-");
                    }
                    dic.Add(spans[0].InnerText.Replace("&nbsp;", ""), value);
                }
            }
            return dic;
        }
    }
}
