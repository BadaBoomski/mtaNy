using System;
using TransponderReceiver;


namespace ConsoleApp3.Domain
{
    public interface ITransponderReceiverClient
    {
        event EventHandler<TrackEvents> ReadyTracks;

        ITransponderReceiver Receiver { get; set; }
    }
}