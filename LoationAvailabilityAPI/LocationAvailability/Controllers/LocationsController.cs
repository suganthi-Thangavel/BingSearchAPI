using LocationAvailability.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocationAvailability.Controllers
{
    [RoutePrefix("api/locations")]
    public class LocationsController : ApiController
    {
        private ILocationService locationService;

        public LocationsController()
        {

        }
        public LocationsController(ILocationService service)
        {
            locationService = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            // Return all locations directly from the service
            var locations = locationService.GetAllLocations();
            return Ok(locations);
        }

        [HttpGet]
        [Route("{from}/{to}")]
        public IHttpActionResult Get(string from, string to)
        {
            var locations = locationService.GetLocationsByAvailability(from, to);
            return Ok(locations);
        }

        
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult AddLocation(Locations newLocation)
        {
            if (newLocation == null)
            {
                return BadRequest("Location object is null");
            }

            // Call the service method to add the new location
            var addedLocation = locationService.AddLocation(newLocation);

            // Return the newly added location
            return Ok(addedLocation);
        }
    }
}
