using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Domain;
using ConsoleApp3.Interfaces;
using ConsoleApp3.Boundary;
using TransponderReceiver;



namespace ConsoleApp3.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            IMonitor monitor = new Monitor();
            ITransponderReceiver reciever = TransponderReceiverFactory.CreateTransponderDataReceiver();
            ITransponderReceiverClient transponder = new TransponderReceiverClient(reciever);
            IOurAirspace ourAirspace = new OurAirspace();
            ICheckTracks checkTracks = new CheckTracks(ourAirspace, transponder);
            ITracksUpdated tracksUpdated = new TracksUpdated(checkTracks);
            ILogger logger = new PLogger();
            ISeparationDetector separationDetector = new SeparationDetector(tracksUpdated,logger);

            //Renders
            ISeperationProvider providerToMonitor = new SeparationProvider(separationDetector, monitor);
            IConvertDataToMonitor dataToMonitor = new ConvertDataToMonitor(tracksUpdated, monitor);
            Console.ReadKey();
        }
    }
}
