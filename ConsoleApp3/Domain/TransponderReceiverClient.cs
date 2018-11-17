using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ConsoleApp3.Domain
{
    public class TransponderReceiverClient
    {
        public event EventHandler<TrackEvents> ReadyTracks;
        private ITransponderReceiver receiver;

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            this.receiver = receiver;

            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady; // this is our tracks
        }

        public void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            var newTrackList = new List<ITrack>();
            foreach (var data in e.TransponderData)
            {
                var split = data.Split(';');
                var newTrack = new Track(split[0], Int32.Parse(split[1]), Int32.Parse(split[2]), DateTime.ParseExact(split[3], "yyyyMMddHHmmssfff", null));
                System.Console.WriteLine($"Transponderdata {data}");

                newTrackList.Add(newTrack);
            }
        }
    }
}
