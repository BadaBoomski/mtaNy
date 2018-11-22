using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;

namespace ConsoleApp3
{

    public class CheckTracks: ICheckTracks
    {
        private IOurAirspace _ourAirspace;
        public event EventHandler<TrackEvents> CheckedTracks;
        public List<ITrack> tracksThatAreChecked; // Maybe not interface?

        public CheckTracks(IOurAirspace ourAirspace, ITransponderReceiverClient trc)

        {
            _ourAirspace = ourAirspace;
            trc.ReadyTracks += TracksThatAreChecked;
        }

        private void TracksThatAreChecked(object sender, TrackEvents e)
        {
            tracksThatAreChecked = new List<ITrack>(); // Maybe not interface?

            foreach (ITrack data in e.TrackData)
            {
                if(_ourAirspace.IsPlaneInOurAirspace(data))
                {
                    tracksThatAreChecked.Add(data);
                }

                var handler = CheckedTracks;
                handler?.Invoke(this, new TrackEvents(tracksThatAreChecked));
            }
        }
    }
}
