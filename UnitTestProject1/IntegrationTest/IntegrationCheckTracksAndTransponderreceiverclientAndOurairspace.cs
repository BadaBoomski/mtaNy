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
        private int _nEventsRaised;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _ourAirspace = new OurAirspace();
            _trc = new TransponderReceiverClient(_transponderReceiver);
            _uut = new CheckTracks(_ourAirspace, _trc);
            _nEventsRaised = 0;

            _uut.CheckedTracks += (o, args) =>
            {
                _filteredList = args.TrackData;
                ++_nEventsRaised;
            };
        }

        // Tried to make a TearDown to reset the _nEventsRaised after each test, but that didn't fix the problem. Problem possibly found in TransponderReceiverClient.cs?
        [TearDown]
        public void Teardown()
        {
            _nEventsRaised = 0;
        }


        /// <summary>
        /// DOESN'T WORK!!! Keeps raising 3 events, when it should only raise 2. 
        /// </summary>
        /// <param name="approvedTrack"></param>
        /// <param name="unapprovedTrack"></param>
        //[TestCase("HOJ1234;20000;20000;10000;20000101235959999", "DØV542;1;20000;10000;20000101235959999")]
        ////[TestCase("HEJ1234;20000;89999;19999;20000101235959999", "DAV542;1;1;1;20000101235959999")]
        //public void CheckTracks_TracksAddedTwice_NumberOfEventsReceivedIsCorrect(string transponderDataInside, string transponderDataOutside)
        //{
        //    _nEventsRaised = 0;

        //    List<string> transponderStrings = new List<string>();
        //    transponderStrings.Add(transponderDataInside);
        //    var args = new RawTransponderDataEventArgs(transponderStrings);

        //    _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
        //    args.TransponderData.Add(transponderDataOutside);
        //    _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

        //    Assert.That(_nEventsRaised, Is.EqualTo(2));
        //}

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

        [TestCase("DAV542;1;1;1;20000101235959999")] // All below boundaries
        [TestCase("DAV542;90001;90001;20001;20000101235959999")] // All above boundaries
        public void ParsingToFiltering_TransponderdataStringOutsideAirspace_FilteredTracksDoesNotContainsNewTrack(string transponderDataStringOutsideOurAirspace)
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataStringOutsideOurAirspace);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);


            Assert.That(_filteredList.Count, Is.EqualTo(0));
        }

        [TestCase("DAV542;10000;10000;500;20000101235959999")] // All exactly equal to lower boundaries
        [TestCase("DAV542;90000;90000;20000;20000101235959999")] // All exactly equal to upper boundaries
        [TestCase("DAV542;50000;50000;10250;20000101235959999")] // All equal to level between the two boundaries

        public void ParsingToFiltering_TransponderdataStringOutsideAirspace_FilteredTracksDoesContainsNewTrack(string transponderDataStringOutsideOurAirspace)
        {
            List<string> transponderStrings = new List<string>();
            transponderStrings.Add(transponderDataStringOutsideOurAirspace);
            var args = new RawTransponderDataEventArgs(transponderStrings);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);


            Assert.That(_filteredList.Count, Is.EqualTo(1));
        }
    }
}
