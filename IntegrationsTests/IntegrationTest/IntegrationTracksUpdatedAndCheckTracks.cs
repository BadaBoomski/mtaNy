using ConsoleApp3;
using ConsoleApp3.Domain;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using TransponderReceiver;

namespace UnitTestProject1.IntegrationTest
{
    [TestFixture]
    public class IntegrationTracksUpdatedAndCheckTracks
    {
        private ITransponderReceiver _transponderReceiver;
        private ITracksUpdated _uut;
        private ICheckTracks _filter;
        private IOurAirspace _ourAirspace;
        private ITransponderReceiverClient _trc;
        private List<ITrack> _updatedList;
        private int _nEventReceived;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _transponderReceiver = Substitute.For<ITransponderReceiver>();

            _ourAirspace = new OurAirspace();
            _trc = new TransponderReceiverClient(_transponderReceiver);
            _filter = new CheckTracks(_ourAirspace, _trc);
            _uut = new TracksUpdated(_filter);
            _uut.TrackUpdated += (o, args) => {
                _updatedList = args.TrackData;
                ++_nEventReceived;
            };

        }

        [TestCase("FAG99;90000;90000;20000;20000101235958999", "FAG99;99999;45001;15000;20000101235958999", "FAG12;10000;10000;5000;20000101235958999", "TAG12;500;54321;12345;20000101235959999", "TAG21;90002;54321;12345;20000101235959999", 100, 100)]
        public void Update_TransponderDataTwice_UpdatedTracks(string track1, string track2, string track3, string track1_1, string track2_1, double course, double velocity)
        {
            //Arrange
            string withinTag = track1.Split(';')[0];
            var transponderData = new List<string>();
            transponderData.Add(track1);
            transponderData.Add(track2);
            transponderData.Add(track3);
            var transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

            //Act
            _transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

            //Re-Arranging, but keeping one plane/track inside our airspace.
            transponderData = new List<string>();
            transponderData.Add(track1_1);
            transponderData.Add(track2_1);
            transponderData.Add(track3);
            transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

            //Assert
            Assert.That(_updatedList.Count == 2, Is.True);
            Assert.That(_updatedList[0].Tag, Is.EqualTo(withinTag));
            Assert.That(_nEventReceived, Is.EqualTo(6));
        }
    }
}
