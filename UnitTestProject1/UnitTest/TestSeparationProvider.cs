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

namespace UnitTestProject1.UnitTest
{
    [TestFixture]
    class TestSeparationProvider
    {
        //Stubs
        private IMonitor _monitor;
        private ISeperationProvider _seperationProvider;
        private ISeparationDetector _separationDetector;
        //Unit under test
        private SeparationProvider _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _monitor = Substitute.For<IMonitor>();
            _separationDetector = Substitute.For<ISeparationDetector>();
            //Unit under test
            _uut = new SeparationProvider(_separationDetector, _monitor);
        }

        [Test]
        public void SeperationProviderTest()
        {
            //create data
            var separationList = new List<ISeparation>();
            var separationOne = new Separation("testfly1", "testfly2", new DateTime(1995, 11, 23, 1, 1, 1));
            var separationTwo = new Separation("testfly3", "testfly4", new DateTime(1995, 1, 1, 1, 1, 1));
            separationList.Add(separationOne);
            separationList.Add(separationTwo);
            var args = new SeparationEvent(separationList);

            //Create event
            _separationDetector.UpdatedSeparations += Raise.EventWith(args);

            //Assert
            _monitor.Received(1).Clear();
            _monitor.Received(1).Write("***Separations***");
            _monitor.Received(1).Write($"Tag1: " + separationOne.FirstTag + " Tag2: " + separationOne.SecondTag + separationOne.TimeStamp);
            _monitor.Received(1).Write($"Tag1: " + separationTwo.FirstTag + " Tag2: " + separationTwo.SecondTag + separationTwo.TimeStamp);
        }
    }
}
