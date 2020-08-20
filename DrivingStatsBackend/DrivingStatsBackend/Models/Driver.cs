using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingStatsBackend.Models
{
    public class Driver
    {
        public string Name { get; set; }

        public static Driver Parse(string s)
        {
            Driver driver = new Driver();

            string[] split = s.Split(" ");

            if (!split[0].Equals("Driver", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentException("Driver must start with 'Driver'.");

            if (split.Length < 2) throw new ArgumentException("Driver is missing a name.");
            driver.Name = split[1];

            if (split.Length > 2) throw new ArgumentException("Driver has an extra symbol after its name '" + split[2] + "'.");

            return driver;
        }
    }
}
