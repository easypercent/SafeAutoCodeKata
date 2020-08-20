using DrivingStatsBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingStatsBackend.Controllers
{
    public class UploadReportLineItem
    {
        public Driver Driver { get; set; }

        public double TotalMilesDriven { get; set; }

        public TimeSpan TotalTimeTaken { get; set; } 

        public double AverageMph 
        {
            get 
            {
                if (TotalTimeTaken.TotalHours == 0) return 0;
                return TotalMilesDriven / TotalTimeTaken.TotalHours;
            }
        }
    }
}
