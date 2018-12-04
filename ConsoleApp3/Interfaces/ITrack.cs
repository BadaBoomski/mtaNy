using System;
using System.Collections.Generic;

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

        void Update(ITrack newData);
        double calCompassCourse(int deltaX, int deltaY);

        List<ITrack> FindTrackInList(List<ITrack> trackList, ITrack data);
    }
}