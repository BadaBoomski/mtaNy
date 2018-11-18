using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;

namespace ConsoleApp3
{
    class CheckTracks: ICheckTracks
    {
        private IOurAirspace _ourAirspace;
        public EventHandler<TrackEvents> CheckedTracks;

        public CheckTracks(IOurAirspace ourAirspace, TransponderReceiverClient trc)
        {
            _ourAirspace = ourAirspace;
            trc.ReadyTracks += TracksThatAreChecked;
        }

        private void TracksThatAreChecked(object sender, TrackEvents e)
        {
            var tracksThatAreChecked = new List<ITrack>();

            foreach (var data in e.TrackData)
            {
                if(_ourAirspace.IsPlaneInOurAirspace(data))
                {

                }
            }
        }
    }
}
