using System;
namespace ConsoleApp3.Domain
{
    internal interface ITracksUpdated
    {
        event EventHandler<TrackEvents> TrackUpdated;
    }
}