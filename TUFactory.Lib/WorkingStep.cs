using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class WorkingStep
    {
        private readonly string machineType;
        private readonly double volume;

        public WorkingStep(string machineType, double volume)
        {
            this.machineType = machineType;
            this.volume = volume;
        }

        public string GetMachineType() => //durch Property zu ersetzen
            machineType;

        public double GetVolume() => //durch Property zu ersetzen
            volume;
    }
}
