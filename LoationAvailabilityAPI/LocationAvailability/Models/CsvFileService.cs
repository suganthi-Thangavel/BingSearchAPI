using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LocationAvailability.Models
{
    public class CsvFileService : ICsvFileService
    {
        private AppDbcontext db;

        public CsvFileService(AppDbcontext dbContext)
        {
            db = dbContext;
        }
        public async Task<CsvFile> GetCsvFileByNameAsync(string fileName)
        {
            return await db.CsvFiles.FirstOrDefaultAsync(f => f.FileName == fileName);
        }

    }
}