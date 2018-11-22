using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3.Domain
{
    class Monitor: IMonitor
    {
        public void Write(string data)
        {
            Console.WriteLine(data);
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}
