using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Domain
{
    class TracksUpdated
    {
        public event EventHandler<TrackEvents> UpdatedTracks;

        private void UpdateTracks(object sender, TrackEvents e)
        {
            //var updatedTracks = new List<ITrack>();

            //foreach (var data in TrackEvents)
            //{
            //    var 
            //}
        }
    }
}
