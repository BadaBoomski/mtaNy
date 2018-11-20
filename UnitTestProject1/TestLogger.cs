using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ConsoleApp3.Boundary;
using ConsoleApp3.Interfaces;

namespace UnitTestProject1
{
    [TestFixture()]
    class TestLogger
    {
        private Logger _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new Logger();
        }


        [TestCase("Tag1 19999101136959128", true)]
        [TestCase(DetteErIkkeEnString, false)]
        public CanTextBeWrittenToFileAndIsFileTextEqualWrittenText(string text, bool result)
        {
            bool testresult =_uut.Log(text, result);
            Assert.That(testresult, IsEqualTo(result));

            var fileText;
            using (StreamReader file = new StreamReader(_filePath))
            {
                fileText = file.ReadLine();
            }

            Assert.That(fileText, IsEqualTo(text));
        }
    }
}
