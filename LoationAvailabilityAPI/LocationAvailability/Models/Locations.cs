using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationAvailability.Models
{
    public class Locations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public TimeSpan AvailabilityFrom { get; set; }
        public TimeSpan AvailabilityTo { get; set; }
    }
}