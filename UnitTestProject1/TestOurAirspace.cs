using System;
using ConsoleApp3;
using ConsoleApp3.Domain;
using NUnit.Framework;
namespace UnitTestProject1
{
    [TestFixture]
    public class TestAirspace
    {

        private OurAirspace _uut;


        [SetUp]
        public void Setup()
        {
           _uut = new OurAirspace();
        }


        [TestCase("ATR423;39045;12932;14000;20151006213456789", true)]
        [TestCase("ATR423;10000;10000;500;20151006213456789", true)]        // equals lower boundary
        [TestCase("ATR423;89999;89999;19999;20151006213456789", true)]      // just below upper boundary
        [TestCase("ATR423;9999;9999;499;20151006213456789", false)]         // all outside lower boundary
        [TestCase("ATR423;90000;90000;20000;20151006213456789", true)]     // all equal upper boundary
        [TestCase("ATR423;45000;9999;14000;20151006213456789", false)]      // YCoor below boundary
        [TestCase("ATR423;9999;45000;14000;20151006213456789", false)]      // XCoor below boundary
        [TestCase("ATR423;45000;45000;200;20151006213456789", false)]       // Altitude below boundary
        [TestCase("ATR423;0;12932;14000;20151006213456789", false)]         // XCoor = 0
        [TestCase("ATR423;39045;0;14000;20151006213456789", false)]         // YCoor = 0
        [TestCase("ATR423;39045;12932;0;20151006213456789", false)]         // Altitude = 0
        [TestCase("ATR423;39045;12932;-5;20151006213456789", false)]        // Altitude is negative
        [TestCase("ATR423;39045;-12932;2000;20151006213456789", false)]     // YCoor is negative (testing Jenkins and webhooks..)
        [TestCase("ATR423;-39045;12932;2000;20151006213456789", false)]     // XCoor is negative (testing Jenkins and webhooks..)
        public void IsPlaneInOurAirSpace_TestingCoordinates_ReturnsCorrectBoolResult(string data, bool result)
        {
            var mustBeInsideAirspace = new Track(data);

            bool testResult = _uut.IsPlaneInOurAirspace(mustBeInsideAirspace);

            // Kept giving errors if the NUnit.Framework wasn't before Assert. Removed using Microsofts UT and it worked.
            // Can be simplifies, but kept it for internal reasons.
            NUnit.Framework.Assert.That(testResult, Is.EqualTo(result));
        }
    }
}
