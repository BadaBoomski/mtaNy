using System.Collections.Generic;
using NUnit.Framework;
using TransponderReceiver;
using ConsoleApp3.Domain;
using ConsoleApp3;
using NSubstitute;

namespace UnitTestProject1
{
    [TestFixture]
    public class IntegrationCheckTracksAndTransponderreceiverclientAndOurairspace
    {
        private ITransponderReceiver _transponderReceiver;
        private ICheckTracks _uut;
        private IOurAirspace _ourAirspace;
        private ITransponderReceiverClient _trc;
        private List<ITrack> _filteredList;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _ourAirspace = new OurAirspace();
            _trc = new TransponderReceiverClient(_transponderReceiver);
            _uut = new CheckTracks(_ourAirspace, _trc);

            _uut.CheckedTracks += (o, args) =>
            {
                _filteredList = args.TrackData;
            };
        }

        [TestCase("HEJ1234;20000;20000;10000;20000101235959999", "DAV542;1;20000;10000;20000101235959999")]
        [TestCase("HEJ1234;20000;89999;19999;20000101235959999", "DAV542;1;1;1;20000101235959999")]
        public void CheckTracks_TestingTransponderdataChecked_InsertApprovedAndUnapprovedTrackMustOnlyAcceptTheApproved(string approvedTrack, string unapprovedTrack)
        {
            // Arrange
            string approvedTracksTag = approvedTrack.Split(';')[0]; // correctTracks tag
            var trcData = new List<string>();
            var correctTrack = approvedTrack;
            var wrongTrack = unapprovedTrack;
            trcData.Add(correctTrack);
            trcData.Add(wrongTrack);
            var transponderDataEventArgs = new RawTransponderDataEventArgs(trcData);


            // Act
            _transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

            // Assert
            Assert.That(_filteredList.Count == 1, Is.True);
            Assert.That(_filteredList[0].Tag, Is.EqualTo(approvedTracksTag));
        }

    }
}
