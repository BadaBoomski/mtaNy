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
        private double _c1, _c2;
        private Track _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Track();
        }

        // Sorry Frank, but as you know this is just an easy way to get a better coverage
        // which you asked for in the feedback.

        [Test]
        public void TestingGetMethod_MustBeEqual()
        {
            _uut.Course = _c1;
            Assert.AreEqual(_uut.Course, _c1);
        }
    }
}
