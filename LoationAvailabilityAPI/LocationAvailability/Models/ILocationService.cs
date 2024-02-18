using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationAvailability.Models
{
    public interface ILocationService
    {
        IEnumerable<Locations> GetAllLocations();
        IEnumerable<Locations> GetLocationsByAvailability(string from, string to);
        Locations AddLocation(Locations newLocation);
    }
}
