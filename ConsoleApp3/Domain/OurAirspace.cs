using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;
using TransponderReceiver;

namespace ConsoleApp3.Domain
{
    public class OurAirspace : IOurAirspace
    {
        public event EventHandler<TrackEvents> TrackInOurAirspace;
        public int SWcornerX { get; set; }
        public int SWcornerY { get; set; }
        public int NEcornerX { get; set; }
        public int NEcornerY { get; set; }
        public int LowerBoundary { get; set; }
        public int UpperBoundary { get; set; }

        public OurAirspace(ITransponderReceiverClient trc)
        {
            trc.ReadyTracks += TrackIsInOurAirspace;
            SWcornerX = 10000;
            SWcornerY = 10000;
            NEcornerX = 90000;
            NEcornerY = 90000;
            LowerBoundary = 500;
            UpperBoundary = 20000;
        }

        private void TrackIsInOurAirspace(object sender, TrackEvents e)
        {
            var tracksInOurAirspace = new List<ITrack>();

            foreach (var track in e.TrackData)
            {
                if (IsPlaneInOurAirspace(track))
                {
                    tracksInOurAirspace.Add(track);
                }
            }

            TracksInOurAirspace(new TrackEvents(tracksInOurAirspace));
        }

        protected virtual void TracksInOurAirspace(TrackEvents e)
        {
            TrackInOurAirspace?.Invoke(this, e);
        }

        public bool IsPlaneInOurAirspace(ITrack checker)
        {
            if (checker.XCoordinate >= SWcornerX && checker.XCoordinate <= NEcornerX &&
                checker.YCoordinate >= SWcornerY && checker.YCoordinate <= NEcornerY &&
                checker.Altitude >= LowerBoundary && checker.Altitude <= UpperBoundary)
            {
                return true;
            }
            return false;
        }
    }
}
