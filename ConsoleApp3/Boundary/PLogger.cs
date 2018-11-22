sing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;
using System.IO;

namespace ConsoleApp3.Boundary
{
    public class PLogger : ILogger
    {

        private readonly string _filePath;

        public PLogger(string filePath = null)
        {
            _filePath = filePath ?? "Log.txt";
        }
        public void Log(string tag)
        {
            using (StreamWriter file = new StreamWriter(_filePath, true))
                file.WriteLine(tag);
        }
    }
}
