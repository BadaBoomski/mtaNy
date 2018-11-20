using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3.Domain
{
    public class Separation : ISeparation
    {
        public string FirstTag { get; }
        public string SecondTag { get; }
        public DateTime TimeStamp { get; }

        public Separation(string firstTag, string secondTag, DateTime timeStamp)
        {
            FirstTag = firstTag;
            SecondTag = secondTag;
            TimeStamp = timeStamp;
        }
    }
}