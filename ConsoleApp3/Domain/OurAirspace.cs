using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Domain
{
    public class OurAirspace : IOurAirspace
    {
        public int SWcornerX { get; set; }
        public int SWcornerY { get; set; }
        public int NEcornerX { get; set; }
        public int NEcornerY { get; set; }
        public int LowerBoundary { get; set; }
        public int UpperBoundary { get; set; }

        public OurAirspace()
        {
            SWcornerX = 10000;
            SWcornerY = 10000;
            NEcornerX = 90000;
            NEcornerY = 90000;
            LowerBoundary = 500;
            UpperBoundary = 20000;
        }

        public bool IsPlaneInOurAirspace(ITrack checker)
        {
            if (checker.XCoordinate >= SWcornerX && checker.XCoordinate <= NEcornerX &&
                checker.YCoordinate >= SWcornerY && checker.YCoordinate <= NEcornerY &&
                checker.Altitude >= LowerBoundary && checker.Altitude <= UpperBoundary)
            {
                return true;
            }

            return false;
        }
    }
}
