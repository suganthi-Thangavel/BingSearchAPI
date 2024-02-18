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
        private readonly IDbContext _dbContext;
        public CsvFileService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CsvFile> GetCsvFileByNameAsync(string fileName)
        {
            return await _dbContext.CsvFiles.FirstOrDefaultAsync(f => f.FileName == fileName);
        }

    }
}