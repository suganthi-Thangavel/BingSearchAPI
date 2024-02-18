using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocationAvailability.Models
{
    public class AppDbcontext: DbContext, IDbContext
    {
        public AppDbcontext() : base("LocationConnectionString")
        {
        }

        public DbSet<Locations> Locations { get; set; }
        public DbSet<CsvFile> CsvFiles { get; set; }
    }
}