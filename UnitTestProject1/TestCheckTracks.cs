using System;
using System.Collections.Generic;
using System.Globalization;
using ConsoleApp3;
using ConsoleApp3.Domain;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;

// Tools, NUGET Pack-Man, Install-Package NSubstitute+Install-Package NSubstitute.Analyzers.CSharp


namespace UnitTestProject1
{
    [TestFixture]
    public class TestCheckTracks
    {
        private CheckTracks _uut;
        private IOurAirspace _ourAirspace;
        private ITransponderReceiverClient _transponderReceiverClient;
        private List<ITrack> _trackList;
        private int _nEventsReceived;

        [SetUp]
        public void Setup()
        {
            // Using NSubstitute to gain control.
            _ourAirspace = new OurAirspace();
            _transponderReceiverClient = Substitute.For<ITransponderReceiverClient>();

            _uut = new CheckTracks(_ourAirspace, _transponderReceiverClient);

            _uut.CheckedTracks += (o, args) =>
            {
                _trackList = args.TrackData;
                ++_nEventsReceived;
            };
        }

        // REUSING same testcases from TestOurAirspace.cs
        [TestCase("ATR423", 39045, 12932, 14000, "20151006213456789", true)]
        [TestCase("ATR323", 10000, 10000, 500, "20151006213456789", true)] // equals lower boundary
        [TestCase("ATR423", 89999, 89999, 19999, "20151006213456789", true)] // just below upper boundary
        [TestCase("ATR623", 9999,9999,499,"20151006213456789", false)] // all outside lower boundary
        [TestCase("ATR423", 90000, 90000,20000,"20151006213456789", true)] // all equal upper boundary
        [TestCase("ATR423", 45000, 9999, 14000, "20151006213456789", false)] // YCoor below boundary
        [TestCase("ATR423", 9999, 45000, 14000, "20151006213456789", false)] // XCoor below boundary
        [TestCase("ATR423", 45000, 45000, 200, "20151006213456789", false)] // Altitude below boundary
        [TestCase("ATR423", 0, 12932, 14000, "20151006213456789", false)] // XCoor = 0
        [TestCase("ATR423", 39045, 0, 14000, "20151006213456789", false)] // YCoor = 0
        [TestCase("ATR423", 39045, 12932, 0, "20151006213456789", false)] // Altitude = 0
        [TestCase("ATR423", 39045, 12932, -5, "20151006213456789", false)] // Altitude is negative
        [TestCase("ATR423", 39045, -12932, 2000, "20151006213456789", false)] // YCoor is negative (testing Jenkins and webhooks..)
        [TestCase("ATR423", -39045, 12932, 2000, "20151006213456789", false)] // XCoor is negative (testing Jenkins and webhooks..)
        public void TracksThatAreChecked_IfCheckedIsTrueThenItWillBeAddedToList_MustReturnCorrectBoolResult(string tag, int x, int y, int alt, string date, bool result)
        {
            // Arrange
            var track = new Track() { Tag = tag, XCoordinate = x, YCoordinate = y, Altitude = alt, Timestamp = DateTime.ParseExact(date, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture) };
            List<ITrack> trackList = new List<ITrack>();
            trackList.Add(track);
           TrackEvents args = new TrackEvents(trackList);

            // Act
            _transponderReceiverClient.ReadyTracks += Raise.EventWith(args);

            // Assert
            Assert.That(_trackList.Contains(track), Is.EqualTo(result));

        }

        [Test]
        public void TracksAreChecked_TrackListContainsMoreThanOneApprovedTrack_MustReturnTrue()
        {
            // Arrange
            var track1 = new Track() { Tag = "ATR323", XCoordinate = 20394, YCoordinate = 46464, Altitude = 1000, Timestamp = DateTime.Now };
            var track2 = new Track() { Tag = "BED323", XCoordinate = 30000, YCoordinate = 20000, Altitude = 2000, Timestamp = DateTime.Now };
            List<ITrack> trackList = new List<ITrack>();
            trackList.Add(track1);
            trackList.Add(track2);
            TrackEvents args = new TrackEvents(trackList);

            // Act
            _transponderReceiverClient.ReadyTracks += Raise.EventWith(args);

            // Assert
            Assert.That(_trackList.Contains(track1) && _trackList.Contains(track2), Is.True);
        }

        [Test]
        public void TracksAreChecked_TrackListDoesNotContainUnapprovedTracks_MustReturnFalse()
        {
            // Arrange
            var track1 = new Track() { Tag = "ATR323", XCoordinate = 1, YCoordinate = 46464, Altitude = 1000, Timestamp = DateTime.Now };
            var track2 = new Track() { Tag = "BED323", XCoordinate = 30000, YCoordinate = 1, Altitude = 2000, Timestamp = DateTime.Now };
            List<ITrack> trackList = new List<ITrack>();
            trackList.Add(track1);
            trackList.Add(track2);
            TrackEvents args = new TrackEvents(trackList);

            // Act
            _transponderReceiverClient.ReadyTracks += Raise.EventWith(args);

            // Assert
            Assert.That(_trackList.Contains(track1) && _trackList.Contains(track2), Is.False);
        }
    }
}