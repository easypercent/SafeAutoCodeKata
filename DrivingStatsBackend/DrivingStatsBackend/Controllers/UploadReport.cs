using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingStatsBackend.Controllers
{
    public class UploadReport
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public List<UploadReportLineItem> Items { get; set; }
    }
}
