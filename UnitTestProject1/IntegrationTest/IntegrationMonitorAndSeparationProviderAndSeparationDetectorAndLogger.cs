using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
using ConsoleApp3.Interfaces;
using NUnit.Framework;
using ConsoleApp3.Domain;
using NSubstitute;

namespace UnitTestProject1
{
    [TestFixture]
    class IntegrationMonitorAndSeparationProviderAndSeparationDetectorAndLogger
    {
        //Stubs
        private IMonitor _monitor;
        private ISeperationProvider _seperationProvider;
        private ISeparationDetector _separationDetector;
        private ITracksUpdated _tracksUpdated;
        private ILogger _Logger;
        private List<ISeparation> _separationsList;

        //Unit under test
        private SeparationDetector _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _monitor = Substitute.For<IMonitor>();
            _separationDetector = Substitute.For<ISeparationDetector>();
            _tracksUpdated = Substitute.For<ITracksUpdated>();
            _Logger = Substitute.For<ILogger>();
            //Unit under test
            _uut = new SeparationDetector(_tracksUpdated, _Logger);
            _seperationProvider = Substitute.For<ISeperationProvider>();

            _uut.UpdatedSeparations += (o, args) =>
            {
                _separationsList = args.SeparationTrack;
           
            };
        }

        //[Test]
        //public void IntegrationTester()
        //{
        //    //create data
        //    List<ITrack> trackList = new List<ITrack>();
        //    string fileText;

        //    Track fly1 = new Track
        //    {
        //        Timestamp = new DateTime(1995, 1, 1, 1, 1, 1),
        //        Tag = "fly1",
        //        Altitude = 700,
        //        YCoordinate = 11000,
        //        XCoordinate = 11000,
        //        Course = 10,
        //        Velocity = 150
        //    };

        //    Track fly2 = new Track
        //    {
        //        Timestamp = new DateTime(1995, 1, 1, 1, 1, 1),
        //        Tag = "fly2",
        //        Altitude = 700,
        //        YCoordinate = 11000,
        //        XCoordinate = 11000,
        //        Course = 10,
        //        Velocity = 150
        //    };

        //    trackList.Add(fly1);
        //    trackList.Add(fly2);
        //    var args = new TrackEvents(trackList);

        //    //Create event
        //    _tracksUpdated.TrackUpdated += Raise.EventWith(args);


        //    //Assert
        //    //_monitor.Received(1).Clear();
        //    _monitor.Received(1).Write("---- Planes Are To Damn Close -----");
        //    _monitor.Received(1).Write(fly1.Tag + " And " + fly2.Tag + " " + new DateTime(1995, 1, 1, 1, 1, 1));
        //    _monitor.Received(1).Write(fly2.Tag + " And " + fly1.Tag + " " + new DateTime(1995, 1, 1, 1, 1, 1));

        //    using (StreamReader file = new StreamReader("log.txt"))
        //    {
        //        fileText = file.ReadLine();
        //    }

        //    Assert.That(fileText, Is.EqualTo(fly1.Tag + " Has started a seperation event with " + fly2.Tag + " ; " + fly1.Timestamp.ToString()));
        //    Assert.That(fileText, Is.EqualTo(fly2.Tag + " Has started a seperation event with " + fly1.Tag + " ; " + fly2.Timestamp.ToString()));

        //}
    }
}
