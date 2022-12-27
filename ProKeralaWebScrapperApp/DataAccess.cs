using Dapper;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace CalendarWebScrapperApp
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\LocationDatabase.sqlite"; }
        }

        public static SQLiteConnection DbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
    public static class DataAccess
    {
        static void CreateTable()
        {
            var connection = SqLiteBaseRepository.DbConnection();
            connection.Open();
            string locationTableSql = "CREATE TABLE IF NOT EXISTS " +
                "Locations (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, LocationCode NVARCHAR(100) NOT NULL, LocationName NVARCHAR(2048) NOT NULL)";
            connection.Execute(locationTableSql);
            connection.Close();

        }
        public static void InitializeDatabase()
        {
            CreateTable();
            var locationObj = new Location();
            var locations = locationObj.GetLocations();

            foreach (var location in locations)
            {
                AddLocation(location);
            }
        }

        public static List<Location> GetLocations()
        {
            using var connection = SqLiteBaseRepository.DbConnection();
            connection.Open();
            string locationTableSql = "SELECT * FROM Locations";
            List<Location> locations = (List<Location>)connection.Query<Location>(locationTableSql);
            return locations;

        }

        public static void AddLocation(Location location)
        {
            var locationsData = GetLocations();
            var data = locationsData.Find(x => x.LocationCode == location.LocationCode);
            if (data == null && !string.IsNullOrEmpty(location.LocationCode))
            {
                using (var connection = SqLiteBaseRepository.DbConnection())
                {
                    string locationTableSql = $"INSERT INTO Locations(LocationName, LocationCode) " +
                        $"VALUES ('{location.LocationName}', '{location.LocationCode}')";
                    connection.Open();
                    connection.Execute(locationTableSql);
                    connection.Close();
                }
            }
        }

        public static void UpdateLocation(Location location)
        {
            if (location.Id != 0 && !string.IsNullOrEmpty(location.LocationCode))
            {
                using (var connection = SqLiteBaseRepository.DbConnection())
                {
                    string locationTableSql = $"UPDATE Locations SET " +
                        $"LocationName = '{location.LocationName}'," +
                        $"LocationCode = '{location.LocationCode}'" +
                        $"WHERE Id={location.Id}";
                    connection.Open();
                    connection.Execute(locationTableSql);
                    connection.Close();
                }
            }
        }

        public static void DeleteLocation(int index)
        {
            using (var connection = SqLiteBaseRepository.DbConnection())
            {
                string locationTableSql = $"DELETE FROM Locations " +
                    $"WHERE Id={index}";
                connection.Open();
                connection.Execute(locationTableSql);
                connection.Close();
            }
        }
    }
}
