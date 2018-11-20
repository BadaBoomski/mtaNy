using System;
namespace ConsoleApp3.Domain
{
    public interface IOurAirspace
    {
        int SWcornerX { get; set; }
        int SWcornerY { get; set; }
        int NEcornerX { get; set; }
        int NEcornerY { get; set; }
        int LowerBoundary { get; set; }
        int UpperBoundary { get; set; }

        bool IsPlaneInOurAirspace(ITrack checker);

        //event EventHandler<TrackEvents> TrackInOurAirspace;

    }
}