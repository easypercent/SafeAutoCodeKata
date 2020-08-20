using DrivingStatsBackend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingStatsBackend.Controllers
{
    public class UploadModel
    {
        public List<Driver> Drivers { get; set; }

        public List<Trip> Trips { get; set; }

        // maps a driver's name to the driver's trips
        public Dictionary<string, List<Trip>> DriverTrips
        {
            get
            {
                Dictionary<string, List<Trip>> driverTrips = new Dictionary<string, List<Trip>>();
                foreach (var driver in Drivers)
                {
                    driverTrips[driver.Name] = new List<Trip>();
                }
                foreach (var trip in Trips)
                {
                    if (driverTrips.ContainsKey(trip.DriverName)) 
                        driverTrips[trip.DriverName].Add(trip);
                }
                return driverTrips;
            }
        }

        public static UploadModel Parse(string s)
        {
            List<Driver> drivers = new List<Driver>();
            List<Trip> trips = new List<Trip>();
            HashSet<string> driverNames = new HashSet<string>(); // prevent duplicate drivers

            StringReader reader = new StringReader(s);
            String line = reader.ReadLine();
            int lineNumber = 1;

            while (line != null)
            {
                string[] split = line.Split(" ");
                string firstPart = split[0];


                if (firstPart.Trim().Length == 0)
                {
                    // empty line - do nothing
                }
                else if (firstPart.Equals("Driver", StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        Driver driver = Driver.Parse(line);
                        if (driverNames.Contains(driver.Name)) throw new ArgumentException("duplicated driver name '" + driver.Name + "'.");
                        driverNames.Add(driver.Name);
                        drivers.Add(driver);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ArgumentException("Error on line " + lineNumber + ": " + e.Message);
                    }
                }
                else if (firstPart.Equals("Trip", StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        Trip trip = Trip.Parse(line);
                        if (trip.AverageMph >= 5 && trip.AverageMph <= 100)
                            trips.Add(trip);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ArgumentException("Error on line " + lineNumber + ": " + e.Message);
                    }
                }
                else
                {
                    throw new ArgumentException("Line " + lineNumber + " doesn't start with 'Driver' or 'Trip'");
                }

                line = reader.ReadLine();
                lineNumber++;
            }

            // could check for trips with drivers who don't exist here

            return new UploadModel()
            {
                Drivers = drivers,
                Trips = trips
            };
        }

    }
}
