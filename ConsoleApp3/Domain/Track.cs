using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Track : ITrack
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }

        public DateTime Timestamp { get; set; }

        public double Velocity { get; set; }
        public double Course { get; set; }

        public Track(string tag, int xCoordinate, int yCoordinate, DateTime timestamp)
        {
            Tag = tag;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Timestamp = timestamp;
        }
    }
}
