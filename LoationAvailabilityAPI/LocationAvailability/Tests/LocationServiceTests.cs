using LocationAvailability.Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocationAvailability.Tests
{
    [TestFixture]
    public class LocationServiceTests
    {
        private Mock<IDbContext> dbContextMock;
        private LocationService locationService;
        private List<Locations> locations;

        [SetUp]
        public void Setup()
        {
            // Initialize a mock for the DbContext
            dbContextMock = new Mock<IDbContext>();

            // Create a mock DbSet for Locations
            locations = new List<Locations>
            {
                new Locations { Id = 1, Name = "Location 1", AvailabilityDate = DateTime.Today, AvailabilityFrom = TimeSpan.FromHours(10), AvailabilityTo = TimeSpan.FromHours(15) },
                new Locations { Id = 2, Name = "Location 2", AvailabilityDate = DateTime.Today, AvailabilityFrom = TimeSpan.FromHours(9), AvailabilityTo = TimeSpan.FromHours(12) },
                new Locations { Id = 3, Name = "Location 3", AvailabilityDate = DateTime.Today, AvailabilityFrom = TimeSpan.FromHours(10), AvailabilityTo = TimeSpan.FromHours(15) }
            };

            var mockDbSet = new Mock<DbSet<Locations>>();
            mockDbSet.As<IQueryable<Locations>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Locations>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Locations>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Locations>>().Setup(m => m.GetEnumerator()).Returns(locations.GetEnumerator());

            dbContextMock.SetupGet(m => m.Locations).Returns(mockDbSet.Object);

            // Initialize the service with the mock IDbContext
            locationService = new LocationService(dbContextMock.Object);
        }

        [Test]
        public void GetAllLocations_Returns_All_Locations()
        {
            // Act
            var result = locationService.GetAllLocations();

            // Assert
            CollectionAssert.AreEqual(locations.ToList(), result.ToList());
        }

        [Test]
        public void GetLocationsByAvailability_Returns_Locations_By_Availability()
        {
            // Arrange
            var from = "2am";
            var to = "3pm";
            var currentTime = DateTime.Now.TimeOfDay;
            var currentDate = DateTime.Today;
            var expectedLocations = new List<Locations>
            {
                new Locations { Id = 1, Name = "Location 1", AvailabilityDate = currentDate, AvailabilityFrom = TimeSpan.FromHours(10), AvailabilityTo = TimeSpan.FromHours(15) },
                new Locations { Id = 3, Name = "Location 3", AvailabilityDate = currentDate, AvailabilityFrom = TimeSpan.FromHours(10), AvailabilityTo = TimeSpan.FromHours(15) }
            };          

            // Act
            var result = locationService.GetLocationsByAvailability(from, to);

            // Assert
            CollectionAssert.AreEqual(expectedLocations.ToList(), result.ToList());
        }
    }
}