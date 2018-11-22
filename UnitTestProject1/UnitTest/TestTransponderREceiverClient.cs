using System;
using System.Collections.Generic;
using ConsoleApp3;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ConsoleApp3.Domain;
using TransponderReceiver;
using NSubstitute;

namespace UnitTestProject1
{
    [TestFixture]
    class TransponderReceiverClientTest
    {

        private List<ITrack> _trackList;
        private int _NrEvents;
        private TransponderReceiverClient _uut;
        private ITransponderReceiver _receiver;

        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new TransponderReceiverClient(_receiver);

            _uut.ReadyTracks += (o, args) =>
            {
                _trackList = args.TrackData;
                _NrEvents++;
            };
        }


        [TestCase("TAG01;23310;44251;11368;19999101136959128")]
        public void CreateTrackAndCheckIfExistsInList(string track)
        {

            var transpondedData = new List<string>();
            transpondedData.Add(track);
            var arg = new RawTransponderDataEventArgs(transpondedData);

            _receiver.TransponderDataReady += Raise.EventWith(arg);

            Assert.That(_trackList[0].Tag, Is.EqualTo(track.Split(';') [0]));
            Assert.That(_trackList[0].XCoordinate, Is.EqualTo(Int32.Parse(track.Split(';')[1])));
            Assert.That(_trackList[0].YCoordinate, Is.EqualTo(Int32.Parse(track.Split(';')[2])));
            Assert.That(_trackList[0].Altitude, Is.EqualTo(Int32.Parse(track.Split(';')[3])));
            Assert.That(_trackList[0].Timestamp, Is.EqualTo(DateTime.ParseExact(track.Split(';')[4], "yyyyMMddHHmmssfff", null)));
            Assert.That(_NrEvents, Is.EqualTo(1));
        }
}
}
