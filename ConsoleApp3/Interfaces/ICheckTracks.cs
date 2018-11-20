using System;

namespace ConsoleApp3.Domain
{
    internal interface ICheckTracks
    {
        event EventHandler<TrackEvents> CheckedTracks;
    }
}