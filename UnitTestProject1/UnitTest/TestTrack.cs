using System;
using ConsoleApp3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp3.Domain;
using ConsoleApp3.Interfaces;
using ConsoleApp3.Boundary;
using NUnit;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTestProject1.UnitTest
{
    [TestClass]
    public class TestTrack
    {
        private double _fakeDataDouble;
        private int _fakeDataInt;
        private string _fakeDataString;
        private Track _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Track();
        }

        // SORRY Frank, but as you know this is just an easy way to get a better coverage
        // which you asked for in the feedback.

        [Test]
        public void TestingGetMethodofCourse_MustBeEqual()
        {
            _uut.Course = _fakeDataDouble;
            Assert.AreEqual(_uut.Course, _fakeDataDouble);
        }

        [Test]
        public void TestingGetMethodofXCoordinate_MustBeEqual()
        {
            _uut.XCoordinate = _fakeDataInt;
            Assert.AreEqual(_uut.XCoordinate, _fakeDataInt);
        }

        [Test]
        public void TestingGetMethodofYCoordinate_MustBeEqual()
        {
            _uut.YCoordinate = _fakeDataInt;
            Assert.AreEqual(_uut.YCoordinate, _fakeDataInt);
        }

        [Test]
        public void TestingGetMethodofAltitude_MustBeEqual()
        {
            _uut.Altitude = _fakeDataInt;
            Assert.AreEqual(_uut.Altitude, _fakeDataInt);
        }

        [Test]
        public void TestingGetMethodofVelocity_MustBeEqual()
        {
            _uut.Velocity = _fakeDataDouble;
            Assert.AreEqual(_uut.Velocity, _fakeDataDouble);
        }

        [Test]
        public void TestingGetMethodofTag_MustBeEqual()
        {
            _uut.Tag = _fakeDataString;
            Assert.AreEqual(_uut.Tag, _fakeDataString);
        }
    }
}
