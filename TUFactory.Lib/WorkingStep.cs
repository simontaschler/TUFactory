using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class WorkingStep
    {
        public string MachineType { get; }
        public double Volume { get; }

        public WorkingStep(string machineType, double volume)
        {
            MachineType = machineType;
            Volume = volume;
        }
    }
}
