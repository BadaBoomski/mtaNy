using System;

namespace ConsoleApp3.Domain
{
    public interface ITransponderReceiverClient
    {
        event EventHandler<TrackEvents> ReadyTracks;
    }
}