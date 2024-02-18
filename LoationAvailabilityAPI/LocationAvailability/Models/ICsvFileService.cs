using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationAvailability.Models
{
    public interface ICsvFileService
    {
        Task<CsvFile> GetCsvFileByNameAsync(string fileName);
    }
}
