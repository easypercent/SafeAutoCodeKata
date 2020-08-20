using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DrivingStatsBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrivingStatsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public UploadReport OnPostUploadAsync(IFormFile fileSelect)
        {
            if (fileSelect == null)
            {
                return UploadController.Error("No file uploaded");
            }

            return UploadController.GenerateReportFrom(fileSelect);
        }

        private static UploadReport GenerateReportFrom(IFormFile fileSelect)
        {
            string extension = Path.GetExtension(fileSelect.FileName);
            if (!extension.Equals(".txt")) return UploadController.Error("File uploaded didn't have .txt extension. Found '" + extension + "'.");

            // transfer file to text
            using var reader = new StreamReader(fileSelect.OpenReadStream());
            var text = new StringBuilder();
            while (!reader.EndOfStream)
            {
                text.AppendLine(reader.ReadLine());
            }

            // transfer text to model
            UploadModel uploadModel;
            try
            {
                uploadModel = UploadModel.Parse(text.ToString());
            } 
            catch (ArgumentException e)
            {
                return UploadController.Error(e.Message);
            }

            // transfer model to report
            Dictionary<string, List<Trip>> driverTrips = uploadModel.DriverTrips;
            List<UploadReportLineItem> items = new List<UploadReportLineItem>();
            foreach (var driver in uploadModel.Drivers) {
                UploadReportLineItem lineItem = new UploadReportLineItem();

                lineItem.Driver = driver;
                List<Trip> trips = driverTrips[driver.Name];
                
                lineItem.TotalMilesDriven = trips.Sum(t => t.MilesDriven);

                double totalHoursTaken = trips.Sum(t => (t.StopTime - t.StartTime).TotalHours);
                lineItem.TotalTimeTaken = TimeSpan.FromHours(totalHoursTaken);

                items.Add(lineItem);
            }
            items = items.OrderByDescending(i => i.TotalMilesDriven).ToList();
            return UploadController.Success(items);
        }

        private static UploadReport Success(List<UploadReportLineItem> items)
        {
            return new UploadReport
            {
                Success = true,
                Error = null,
                Items = items
            };
        }

        private static UploadReport Error(string error)
        {
            return new UploadReport
            {
                Success = false,
                Error = error,
                Items = null
            };
        }
    }
}
