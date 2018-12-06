﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
using ConsoleApp3.Interfaces;
using NUnit.Framework;
using ConsoleApp3.Domain;
using NSubstitute;

namespace UnitTestProject1.UnitTest
{
    [TestFixture]
    class TestSeparationProvider
    {
        //Stubs
        private IMonitor _monitor;
        private ISeperationProvider _seperationProvider;
        private ISeparationDetector SepDet;
        //Unit under test
        private SeparationProvider _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _monitor = Substitute.For<IMonitor>();
            SepDet = Substitute.For<ISeparationDetector>();
            //Unit under test
            _uut = new SeparationProvider(SepDet, _monitor);
        }
        [Test]
        public void TestIfPlanesAreTooClose()
        {
            var Separation1 = new Separation("irelevantfly1", "irelevantfly2", new DateTime(1995, 11, 23, 1, 1, 1));

            SeparationDetector SepDet = new SeparationDetector(null,null);

            DateTime tempTime = DateTime.Now;
            Track fly1 = new Track
            {
                Timestamp = tempTime,
                Tag = "fly1",
                Altitude = 700,
                YCoordinate = 11000,
                XCoordinate = 11000,
                Course = 16,
                Velocity = 150
            };
            Track fly2 = new Track
            {
                Timestamp = tempTime,
                Tag = "fly2",
                Altitude = 700,
                YCoordinate = 11000,
                XCoordinate = 11000,
                Course = 16,
                Velocity = 150
            };
            Track fly3 = new Track
            {
                Timestamp = tempTime,
                Tag = "fly3",
                Altitude = 1500,
                YCoordinate = 18000,
                XCoordinate = 18000,
                Course = 16,
                Velocity = 150
            };
            
            Assert.That(SepDet.IsTracksToClose(fly1, fly2), Is.True);
            Assert.That(SepDet.IsTracksToClose(fly1, fly3), Is.False);

        }

        [Test]
        public void SeperationProviderTest()
        {
            //create data
            var separationList = new List<ISeparation>();
            var Separation1 = new Separation("testfly1", "testfly2", new DateTime(1995, 11, 23, 1, 1, 1));
            var Separation2 = new Separation("testfly3", "testfly4", new DateTime(1995, 1, 1, 1, 1, 1));
            separationList.Add(Separation1);
            separationList.Add(Separation2);
            var args = new SeparationEvent(separationList);

            //Create event
            SepDet.UpdatedSeparations += Raise.EventWith(args);

            Assert.That(separationList.Count, Is.EqualTo(2));
            //Assert
            _monitor.Received(1).Clear();
            _monitor.Received(1).Write("---- Planes Are To Damn Close -----");
            _monitor.Received(1).Write(Separation1.FirstTag + " And " + Separation1.SecondTag + " " + Separation1.TimeStamp);
            _monitor.Received(1).Write(Separation2.FirstTag + " And " + Separation2.SecondTag + " " + Separation2.TimeStamp);
        }
    }
}
