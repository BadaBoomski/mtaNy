﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;

namespace ConsoleApp3.Domain
{
   public class TracksUpdated : ITracksUpdated
    {
        public event EventHandler<TrackEvents> TrackUpdated;
        private List<ITrack> oldTracks;

        public TracksUpdated(ICheckTracks checker)
        {
            oldTracks = new List<ITrack>();
            checker.CheckedTracks += UpdateTrack;
        }

        private void UpdateTrack(object sender, TrackEvents e)
        {
            var newTrackList = new List<ITrack>();

            foreach (var track in e.TrackData)
            {

                oldTracks = track.FindTrackInList(oldTracks, track);

            }
            UpdatedTrackEvent(new TrackEvents(oldTracks));
        }


        protected virtual void UpdatedTrackEvent(TrackEvents e)
        {
            TrackUpdated?.Invoke(this, e);
        }
        
        /*public static void Update(ITrack newData)
        {
            var deltaX = newData.XCoordinate - XCoordinate;
            var deltaY = newData.YCoordinate - YCoordinate;
            Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            CompassCourse = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;
            XCoordinate = newData.XCoordinate;
            YCoordinate = newData.YCoordinate;
            Altitude = newData.Altitude;
            TimeStamp = newData.TimeStamp;
        }*/
    }
}
