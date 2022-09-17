using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKeralaWebScrapperApp
{
    public class Location
    {
        public string locationCode { get; set; }
        public string locationName { get; set; }

        public Location() { }

        public Location(string locationCode, string locationName)
        {
            this.locationCode = locationCode;
            this.locationName = locationName;
        }


        public List<Location> GetLocations()
        {
            Location location1 = new Location("4930956", "Boston");
            Location location2 = new Location("4930956", "Chicago");
            Location location3 = new Location("4930956", "Phoenix");
            Location location4 = new Location("4930956", "Los Angeles");

            List<Location> locations = new List<Location>();
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);

            return locations;
        }
    }
}
