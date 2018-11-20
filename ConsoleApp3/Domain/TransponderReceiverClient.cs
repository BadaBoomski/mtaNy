using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ConsoleApp3.Domain
{
    public class TransponderReceiverClient : ITransponderReceiverClient
    {
        public event EventHandler<TrackEvents> ReadyTracks;
        private ITransponderReceiver receiver { set; get; }

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            this.receiver = receiver;

            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady; 
        }

        public ITransponderReceiver Receiver
        {
            get{ return receiver; }
            set{ receiver = value; }
        }

        public void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            var newTrackList = new List<ITrack>();
            foreach (var data in e.TransponderData)
            {
                ITrack newTrack = new Track(data);

                //var split = data.Split(';');
                //var newTrack = new Track(split[0], Int32.Parse(split[1]), Int32.Parse(split[2]), Int32.Parse(split[3]), DateTime.ParseExact(split[4], "yyyyMMddHHmmssfff", null));
                //System.Console.WriteLine($"Transponderdata {data}");

                newTrackList.Add(newTrack);


                // WONT WORK!! Above used instead!
                //track.ProcessTrackData(track);

                //var split = data.Split(';');
                //var newTrack = new Track(split[0], Int32.Parse(split[1]), Int32.Parse(split[2]), Int32.Parse(split[3]), DateTime.ParseExact(split[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture), null);
                //System.Console.WriteLine($"Transponderdata {data}");
            }

            NewTrackEvent(new TrackEvents(newTrackList));
        }

        protected virtual void NewTrackEvent(TrackEvents e)
        {
            ReadyTracks?.Invoke(this, e);
        }
    }
}
