using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Domain
{
    class TracksUpdated : ITrack
    {
        public event EventHandler<TrackEvents> UpdatedTracks;

        private void UpdateTracks(object sender, TrackEvents e)
        {
            //var updatedTracks = new List<ITrack>();

            //foreach (var data in TrackEvents)
            //{
            //    var 
            //}

            public void Update(ITrack newData)
            {
                var deltaX = newData.XCoordinate - XCoordinate;
                var deltaY = newData.YCoordinate - YCoordinate;
                Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                CompassCourse = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;
                XCoordinate = newData.XCoordinate;
                YCoordinate = newData.YCoordinate;
                Altitude = newData.Altitude;
                TimeStamp = newData.TimeStamp;
            }
        }
    }
}
