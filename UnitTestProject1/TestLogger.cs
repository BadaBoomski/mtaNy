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
        private PLogger _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new PLogger();
        }


        [TestCase("Tag1 19999101136959128", true)]
        public void CanTextBeWrittenToFileAndIsFileTextEqualWrittenText(string text)
        {
            _uut.Log(text);
            string fileText;
            using (StreamReader file = new StreamReader("log.txt"))
            {
                fileText = file.ReadLine();
            }

            Assert.That(fileText, Is.EqualTo(text));
        }
    }
}
