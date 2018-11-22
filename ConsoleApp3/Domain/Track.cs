using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Track : ITrack
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }

        public DateTime Timestamp { get; set; }

        public double Velocity { get; set; }
        public double Course { get; set; }

        //public Track(string tag, int xCoordinate, int yCoordinate, int altitude, DateTime timestamp)
        //{
        //    Tag = tag;
        //    XCoordinate = xCoordinate;
        //    YCoordinate = yCoordinate;
        //    Timestamp = timestamp;
        //    Altitude = altitude;
        //}

        //public Track(string rawData)
        //{
        //    string[] delimiters = { ";" };
        //    string[] subStrings = rawData.Split(delimiters, StringSplitOptions.None);

        //    Tag = subStrings[0];
        //    XCoordinate = Int32.Parse(subStrings[1]);
        //    YCoordinate = Int32.Parse(subStrings[2]);
        //    Altitude = Int32.Parse(subStrings[3]);
        //    Timestamp = DateTime.ParseExact(subStrings[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
        //    Velocity = 0;
        //    Course = 0;
        //}

        public void Update(ITrack newData)
        {
            var deltaX = newData.XCoordinate - XCoordinate;
            var deltaY = newData.YCoordinate - YCoordinate;
            var deltaTime = newData.Timestamp - Timestamp;
            Course = this.calCompassCourse(deltaX, deltaY);
            Velocity = this.calVelocity(deltaX, deltaY, deltaTime);
            XCoordinate = newData.XCoordinate;
            YCoordinate = newData.YCoordinate;
            Altitude = newData.Altitude;
            Timestamp = newData.Timestamp;
        }

        public List<ITrack> FindTrackInList(List<ITrack> trackList, ITrack track)
        {
            if (trackList.Exists(p => p.Tag == track.Tag))
            //if (trackList.Contains(track)) problem fordi selvom track har samme Tag så gør timedate de er anderledes derfor tilføjes de
            {
                foreach (var t in trackList)
                {
                    if ((t.Tag == track.Tag) && (t.Timestamp != track.Timestamp))
                    {
                        t.Update(track);
                    }
                }
                

            }
            else
            {
                trackList.Add(track);

            }
            //1 forsøg
            //foreach (var Track in trackList)
            //{
            //    if (track.Tag == Track.Tag)
            //    {
            //        Track.Update(Track);
            //    }
            //    else
            //    {
            //        trackList.Add(Track);
            //    }

            //}
            return trackList;
        }

        public double calCompassCourse(int deltaX, int deltaY)
        {
            Course = ((Math.Atan2(deltaY, deltaX)) * (180 / Math.PI));

            if ((deltaX == 0) && (deltaY == 0))
            {
                return Double.NaN;
            }

            //else if (deltaX == 0)
            //{
            //    if (deltaY < 0)
            //    {
            //        Course = 180;
            //        //        Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            //    }
            //    else
            //    {
            //        Course = 0;
            //    }
            //}

            //else if (deltaY == 0)
            //{
            //    if (deltaX < 0)
            //    {
            //        Course = 270;
            //    }
            //    else
            //    {
            //        Course = 90;
            //    }
            //}

            //else if (deltaX < 0)
            //{
            //    if (deltaY < 0)
            //    {
            //        Course += 180;
            //    }
            //    else
            //    {
            //        Course += 270;
            //    }
            //}

            //else if (deltaX > 0)
            //{
            //    if (deltaY < 0)
            //    {
            //        Course += 90;
            //    }
            //}

            return Course;
        }

        public double calVelocity(int deltaX, int deltaY, TimeSpan deltaTime)
        {
            var timedifference = deltaTime.TotalSeconds;
            var distanceTraveled = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            double velocity = distanceTraveled / timedifference;
            return velocity;
        }

        //public void ProcessTrackData(TrackData trackData)
        //{
        //    if (planeList.Exists(p => p.Data.Tag == trackData.Tag))
        //    {
        //        // update track data.
        //        if (insideAirspace(trackData))
        //        {
        //            planeList.Find(p => p.Data.Tag == trackData.Tag).Data.Update(trackData);
        //        }
        //        else
        //        {
        //            planeList.RemoveAll(p => p.Data.Tag == trackData.Tag);
        //        }
        //    }
        //    else if (insideAirspace(trackData))
        //    {
        //        planeList.Add(new Plane(trackData));
        //    }
    }
}