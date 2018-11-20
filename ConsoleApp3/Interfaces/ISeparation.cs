using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Interfaces
{
    public interface ISeparation
    {
        string FirstTag { get; }
        string SecondTag { get; }
        DateTime TimeStamp { get; }
    }
}