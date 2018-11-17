using System;
using System.Collections.Generic;

namespace ConsoleApp3.Domain
{
    public class TrackEvents: EventArgs
    {
        public List<ITrack> TrackData;

        public TrackEvents(List<ITrack> trackData)
        {
            TrackData = trackData;
        }
    }
}