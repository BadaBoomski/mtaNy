using System;
using ConsoleApp3.Domain;

namespace ConsoleApp3
{
    public interface ICheckTracks
    {
        event EventHandler<TrackEvents> CheckedTracks;

    }
}