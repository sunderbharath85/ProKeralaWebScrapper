using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarWebScrapperApp
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }

        public Location() { }

        public Location(string locationCode, string locationName)
        {
            this.LocationCode = locationCode;
            this.LocationName = locationName;
        }


        public List<Location> GetLocations()
        {
            Location location1 = new Location("4930956", "Boston");
            Location location2 = new Location("4887398", "Chicago");
            Location location3 = new Location("5308655", "Phoenix");
            Location location4 = new Location("5368361", "Los Angeles");
            Location location5 = new Location("", "Other");

            List<Location> locations = new List<Location>() { location1, location2, location3, location4, location5 };
            
            return locations;
        }
    }
}
