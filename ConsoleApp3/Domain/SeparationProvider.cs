using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3.Domain
{
   public class SeparationProvider : ISeparation
   {
       private IMonitor _monitor;

       public SeparationProvider(ISeparationDetector detector, IMonitor monitor)
       {
           _monitor = monitor;
           detector.UpdatedSeparations += ProvideSeparation;
       }

       private void ProvideSeparation(object sender, SeparationEvent e)
       {
           _monitor.Clear();
           _monitor.Write("---- Planes Are To Damn Close -----");
           foreach (var s in e.SeparationTrack)
           {
               var TracksInfo = s.FirstTag + " And " + s.SecondTag + " " + s.TimeStamp;
               _monitor.Write(TracksInfo);
           }
       }
   }
}
