using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ConsoleApp3.Domain;
using ConsoleApp3.Interfaces;

namespace UnitTestProject1
{
    [TextFixture()]
    class TransponderReceiverClientTest
    {
        private ITransponderReceiverClient _receiver;
        private TransponderReceiverClient _uut;

        private List<ITrack> _trackList;
        private int _NrEvents;



        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new TransponderReceiverClient(_receiver);

            -uut.TracksReady += (o, args) =>
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

            _receiver.TransponderDataReady += RaiseEventWith(arg);

            Assert.That(_trackList[0].Tag, Is.EqualTo(track.Split(';') [0]));
            Assert.That(_trackList[0].Position.X, Is.EqualTo(Int32.Parse(track.Split(';')[1])));
            Assert.That(_trackList[0].Position.Y, Is.EqualTo(Int32.Parse(track.Split(';')[2])));
            Assert.That(_trackList[0].Position.Altitude, Is.EqualTo(Int32.Parse(track.Split(';')[3])));
            Assert.That(_trackList[0].Timestamp, Is.EqualTo(DateTime.ParseExact(track.Split(';')[4], "yyyyMMddHHmmssfff", null)));
            Assert.That(_NrEvents, Is.EqualTo(1));
        }







}
}
