using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;
using TransponderReceiver;



namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            IMonitor monitor = new Monitor();
            ITransponderReceiver transponder = TransponderReceiverFactory.CreateTransponderDataReceiver();
        }
    }
}
