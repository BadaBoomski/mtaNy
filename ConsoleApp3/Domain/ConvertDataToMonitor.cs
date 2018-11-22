using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3.Domain
{
    class ConvertDataToMonitor : IConvertDataToMonitor
    {
        private IMonitor _monitor;
        public ConvertDataToMonitor(ITracksUpdated update, IMonitor monitor)
        {
            _monitor = monitor;
            update.TrackUpdated += DataToMonitor;
        }

        private void DataToMonitor(object sender, TrackEvents e)
        {
            _monitor.Write("***Tracks***");
            foreach (var track in e.TrackData)
            {
              
                var str = "Tag: " + track.Tag + " CurrentPosition: " + track.XCoordinate + "mE," +
                          track.YCoordinate +
                          "mN Altitude: " + track.Altitude + "m Velocity: " +
                          Math.Round(track.Velocity, 2) + "m/s Course: " +
                          Math.Round(track.Course, 2) + "°";
                _monitor.Write(str);
            }
        }
    }
}
