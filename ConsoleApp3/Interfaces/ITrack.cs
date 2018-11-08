using System;

namespace ConsoleApp3
{
    public interface ITrack
    {
        string Tag { get; set; }
        int XCoordinate { get; set; }
        int YCoordinate { get; set; }
        int Altitude { get; set; }

        DateTime Timestamp { get; set; }

        double Velocity { get; set; }
        double Course { get; set; }
    }
}