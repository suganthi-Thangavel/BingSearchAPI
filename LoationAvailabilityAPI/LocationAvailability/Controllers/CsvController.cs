using LocationAvailability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace LocationAvailability.Controllers
{
    [RoutePrefix("api/csv")]
    public class CsvController : ApiController
    {
        private readonly ICsvFileService csvService;

        public CsvController(ICsvFileService service)
        {
            this.csvService = service;
        }

        [HttpGet]
        [Route("file")]
        public async Task<HttpResponseMessage> GetCsvFileFromDatabase()
        {
            try
            {
                var csvFile = await csvService.GetCsvFileByNameAsync("xxxx.csv");

                if (csvFile != null)
                {
                    // Convert binary data to CSV format
                    string csvContent = System.Text.Encoding.Default.GetString(csvFile.CsvFileData);

                    // Create HttpResponseMessage containing the CSV file
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(csvContent);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = csvFile.FileName
                    };

                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "CSV file not found in the database.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while retrieving the CSV file: " + ex.Message);
            }
        }
    }
}
