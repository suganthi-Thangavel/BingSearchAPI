using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationAvailability.Models
{
    public class LocationService : ILocationService
    {
        private readonly IDbContext _dbContext;
        public LocationService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Locations> GetAllLocations()
        {
            return _dbContext.Locations.ToList();
        }

        public IEnumerable<Locations> GetLocationsByAvailability(string from, string to)
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var currentDate = DateTime.Today;

            return db.Locations
                .Where(location =>
                    location.AvailabilityDate.Date == currentDate &&
                    IsAvailableNow(location.AvailabilityFrom, location.AvailabilityTo, ParseTimeString(from), ParseTimeString(to)))
                .Select(location =>
                    new Locations
                    {
                        Id = location.Id,
                        Name = location.Name,
                        AvailabilityDate = location.AvailabilityDate,
                        AvailabilityFrom = location.AvailabilityFrom,
                        AvailabilityTo = location.AvailabilityTo
                    })
                .ToList();
        }
        public Locations AddLocation(Locations newLocation)
        {
            // Add the new location to the database
            _dbContext.Locations.Add(newLocation);
            _dbContext.SaveChangesAsync();

            // Return the newly added location
            return newLocation;
        }
        private bool IsAvailableNow(TimeSpan locationFrom, TimeSpan locationTo, TimeSpan requestFrom, TimeSpan requestTo)
        {
            return requestFrom >= locationFrom && requestTo <= locationTo;
        }

        private TimeSpan ParseTimeString(string timeString)
        {
            int hour =0;
            if (int.TryParse(timeString.TrimEnd("amp".ToCharArray()), out hour))
            {
                if (timeString.EndsWith("pm", StringComparison.OrdinalIgnoreCase) && hour < 12)
                {
                    hour += 12;
                }
                else if (timeString.EndsWith("am", StringComparison.OrdinalIgnoreCase) && hour == 12)
                {
                    hour = 0;
                }              

                //var timeFormat = timeString.EndsWith("am", StringComparison.OrdinalIgnoreCase) ? "hhtt" : "hhtt";
                //var time = DateTime.ParseExact(timeString, timeFormat, System.Globalization.CultureInfo.InvariantCulture);
                //return time.TimeOfDay;
            }
            return TimeSpan.FromHours(hour);
        }

        private string ConvertTimeSpanToString(TimeSpan timeSpan)
        {
            // Convert TimeSpan to formatted string (e.g., "10 am")
            return $"{(timeSpan.Hours % 12 == 0 ? 12 : timeSpan.Hours % 12)} {(timeSpan.Hours < 12 ? "am" : "pm")}";
        }

        #region Testing locally without connecting to database


        //private static readonly List<Locations> locations = new List<Locations>
        //{
        //    new Locations { Id = 1, Name = "Location 1", AvailabilityDate = DateTime.Today, AvailabilityFrom = TimeSpan.FromHours(10), AvailabilityTo = TimeSpan.FromHours(15) },
        //    new Locations { Id = 2, Name = "Location 2", AvailabilityDate = DateTime.Today, AvailabilityFrom = TimeSpan.FromHours(9), AvailabilityTo = TimeSpan.FromHours(12) },
        //    new Locations { Id = 3, Name = "Location 3", AvailabilityDate = DateTime.Today, AvailabilityFrom = TimeSpan.FromHours(10), AvailabilityTo = TimeSpan.FromHours(15) }
        //};

        #endregion
    }
}