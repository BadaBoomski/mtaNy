using System;
using ConsoleApp3.Domain;

namespace ConsoleApp3
{
    internal interface ICheckTracks
    {
        event EventHandler<TrackEvents> CheckedTracks;

    }
}