using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;

namespace ConsoleApp3.Domain
{
    class TracksUpdated : ITracksUpdated
    {
        public event EventHandler<TrackEvents> UpdatedTracks;
        private List<ITrack> _oldTrack;

        private void UpdateTracks(object sender, TrackEvents e)
        {
            //var updatedTracks = new List<ITrack>();

            //foreach (var data in TrackEvents)
            //{
            //    var 
            //}

         
        }

        //Itrack someTrack = new ITrack()
        // someTrack.Update(Track data, Track data)
        public static void Update(ITrack newData, ITrack oldData)
        {
            var deltaX = newData.XCoordinate - oldData.XCoordinate;
            var deltaY = newData.YCoordinate - oldData.YCoordinate;
            Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            CompassCourse = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;
            XCoordinate = newData.XCoordinate;
            YCoordinate = newData.YCoordinate;
            Altitude = newData.Altitude;
            TimeStamp = newData.TimeStamp;
        }
    }
}
