using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ConsoleApp3.Domain
{
    public class TransponderReceiverClient
    {
        private ITransponderReceiver receiver;

        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            this.receiver = receiver;

            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        public void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                System.Console.WriteLine($"Transponderdata {data}");
            }
        }
    }
}
