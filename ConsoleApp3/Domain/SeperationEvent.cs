using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Interfaces;

namespace ConsoleApp3
{
    public class SeparationEvent : EventArgs
    {
        public List<ISeparation> SeparationTrack;
        public SeparationEvent(List<ISeparation> separations)
        {
            SeparationTrack = separations;
        }

    }
}