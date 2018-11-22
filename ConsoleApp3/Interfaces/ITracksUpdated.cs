using System;
namespace ConsoleApp3.Domain
{
    public interface ITracksUpdated
    {
        event EventHandler<TrackEvents> TrackUpdated;
    }
}