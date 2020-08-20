using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingStatsBackend.Models
{
    public class Trip
    {
        public string DriverName { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan StopTime { get; set; }

        public double MilesDriven { get; set; }

        public double AverageMph
        {
            get
            {
                return MilesDriven / (StopTime - StartTime).TotalHours;
            }
        }

        public static Trip Parse(string s)
        {
            Trip trip = new Trip();

            string[] split = s.Split(" ");

            if (!split[0].Equals("Trip", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentException("Trip must start with 'trip'.");

            if (split.Length < 2) throw new ArgumentException("Trip is missing a driver's name.");
                trip.DriverName = split[1];

            if (split.Length < 3) throw new ArgumentException("Trip is missing a start time (mm::ss).");
            if (!TimeSpan.TryParse(split[2], out TimeSpan startTime)) throw new ArgumentException("Trip has a malformed start time '" + split[2] + "'."); ;
            trip.StartTime = startTime;

            if (split.Length < 4) throw new ArgumentException("Trip is missing an end time (mm::ss).");
            if (!TimeSpan.TryParse(split[3], out TimeSpan stopTime)) throw new ArgumentException("Trip has a malformed stop time '" + split[3] + "'.");
            trip.StopTime = stopTime;

            if (split.Length < 5) throw new ArgumentException("Trip is missing miles driven (double).");
            if (!double.TryParse(split[4], out double milesDriven)) throw new ArgumentException("Trip has a malformed miles driven '" + split[4] + "'.");
            trip.MilesDriven = milesDriven;

            if (split.Length > 5) throw new ArgumentException("Trip has an extra symbol after its miles driven '" + split[5] + "'.");

            return trip;
        }
    }
}
