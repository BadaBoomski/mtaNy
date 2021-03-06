﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
using ConsoleApp3.Domain;
using NSubstitute;
using NUnit.Framework;

namespace UnitTestProject1.UnitTest
{
    [TestFixture]
    class TestTrackUpdated
    {
        private ICheckTracks _checkTracks;
        private TracksUpdated _uut;
        private int nEvents;
        private List<ITrack> _trackList;



        [SetUp]
        public void SetUp()
        {
            _checkTracks = Substitute.For<ICheckTracks>();

            _uut = new TracksUpdated(_checkTracks);
            _uut.TrackUpdated += (o, args) =>
            {
                _trackList = args.TrackData;
                ++nEvents;
            };
        }

        [Test]
        public void UpdatingTheListOfTracks()
        {
            DateTime tempTime = DateTime.Now;
            List<ITrack> tracklist = new List<ITrack>();
            List<ITrack> Resultlist = new List<ITrack>();
            Track fly1 = new Track
            {
                Timestamp = tempTime,
                Tag = "fly1",
                Altitude = 700,
                YCoordinate = 10000,
                XCoordinate = 10000,
                Course = 15,
                Velocity = 200
            };

            Track fly2 = new Track
            {
                Timestamp = tempTime,
                Tag = "fly2",
                Altitude = 800,
                YCoordinate = 12000,
                XCoordinate = 12000,
                Course = 16,
                Velocity = 150
            };
            tracklist.Clear();
            tracklist.Add(fly1);
            tracklist.Add(fly2);

            var arg = new TrackEvents(tracklist);

            _checkTracks.CheckedTracks += Raise.EventWith(arg);

            Track fly3 = new Track
            {
                Timestamp = tempTime.AddSeconds(1),
                Tag = "fly1",
                Altitude = 600,
                YCoordinate = 10000,
                XCoordinate = 10001,
                Course = 15,
                Velocity = 200
            };

            Track fly4 = new Track
            {
                Timestamp = tempTime.AddSeconds(1),
                Tag = "fly2",
                Altitude = 700,
                YCoordinate = 11000,
                XCoordinate = 11000,
                Course = 16,
                Velocity = 150
            };

            Track notInListPlane = new Track
            {
                Timestamp = DateTime.Now,
                Tag = "NotInListPlane",
                Altitude = 999,
                YCoordinate = 15000,
                XCoordinate = 15000,
                Course = 0,
                Velocity = 0
            };

            tracklist = fly3.FindTrackInList(tracklist, fly3);
            tracklist = fly4.FindTrackInList(tracklist, fly4);
            tracklist = notInListPlane.FindTrackInList(tracklist, notInListPlane);
            //tracklist.Add(fly3);
            //tracklist.Add(fly4);

            var args = new TrackEvents(tracklist);

            _checkTracks.CheckedTracks += Raise.EventWith(args);
            // fly1
            Assert.That(tracklist[0].Altitude, Is.EqualTo(600));
            Assert.That(tracklist[0].YCoordinate, Is.EqualTo(10000));
            Assert.That(tracklist[0].XCoordinate, Is.EqualTo(10001));
            Assert.That(tracklist[0].Timestamp, Is.EqualTo(fly3.Timestamp));
            Assert.That(tracklist[0].Velocity, Is.EqualTo(1));
            Assert.That(tracklist[0].Course, Is.EqualTo(0));


            // fly2
            Assert.That(tracklist[1].Altitude, Is.EqualTo(700));
            Assert.That(tracklist[1].YCoordinate, Is.EqualTo(11000));
            Assert.That(tracklist[1].XCoordinate, Is.EqualTo(11000));
            Assert.That(tracklist[1].Timestamp, Is.EqualTo(fly4.Timestamp));

            Assert.That(tracklist[2].Altitude, Is.EqualTo(999));
            Assert.That(tracklist[2].YCoordinate, Is.EqualTo(15000));
            Assert.That(tracklist[2].XCoordinate, Is.EqualTo(15000));
            Assert.That(tracklist[2].Timestamp, Is.EqualTo(notInListPlane.Timestamp));
            Assert.That(tracklist[2].Velocity, Is.EqualTo(0));
       


        }
    }
}
