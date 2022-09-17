
namespace ProKeralaWebScrapperApp
{
    public class TimeSpan
    {
        public string displayMS { get; set; }
        public int durationMS { get; set; }

        public IList<TimeSpan> timeSpans { get; set; }

        public TimeSpan()
        {
            
        }

        public List<TimeSpan> GetTimeSpans()
        {
            var timeSpans = new List<TimeSpan>()
            {
                new TimeSpan() { displayMS = "0 Secs", durationMS = 0},
                new TimeSpan() { displayMS = "3 Secs", durationMS = 3},
                new TimeSpan() { displayMS = "5 Secs", durationMS = 5},
                new TimeSpan() { displayMS = "10 Secs", durationMS = 10},
                new TimeSpan() { displayMS = "15 Secs", durationMS = 15},
            };
            return timeSpans;
        }
    }
    public class Day
    {
        public string vikramSamvat { get; set; }
        public string indianCivilCalendar { get; set; }
        public string purnimantaMonth  { get; set; }
        public string amantaMonth { get; set; }

        public Dictionary<string, string> tithi { get; set; }

        public Dictionary<string, string> nakshatra { get; set; }

        public Dictionary<string, string> karana { get; set; }

        public Dictionary<string, string> yoga { get; set; }

        public string vara { get; set; }

        public Dictionary<string, string> sunAndMoonTiming { get; set; }

        public Dictionary<string, string> inauspiciousPeriod { get; set; }

        public Dictionary<string, string> auspiciousPeriod { get; set; }

        public Dictionary<string, string> anandadiYoga { get; set; }

        public string sooryaRasi { get; set; }
        public string chandraRasi { get; set; }

        public Dictionary<string, string> lunarMonth { get; set; }
        public string tamilYoga { get; set; }
        public string chandrashtama { get; set; }

        public Dictionary<string, string> others { get; set; }
    }


    public class FlatDay
    {
        public string gregorianDate {get;set;}
        public string vikramSamvat { get; set; }
        public string indianCivilCalendar { get; set; }
        public string purnimantaMonth { get; set; }
        public string amantaMonth { get; set; }
        public string tithi { get; set; }
        public string nakshatra { get; set; }
        public string karana { get; set; }
        public string yoga { get; set; }
        public string vara { get; set; }
        public string sunAndMoonTiming { get; set; }
        public string inauspiciousPeriod { get; set; }
        public string auspiciousPeriod { get; set; }
        public string anandadiYoga { get; set; }
        public string sooryaRasi { get; set; }
        public string chandraRasi { get; set; }
        public string lunarMonth { get; set; }
        public string tamilYoga { get; set; }
        public string chandrashtama { get; set; }
        public string others { get; set; }
    }

}
