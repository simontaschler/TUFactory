using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class WorkingStep
    {
        private string machineType;
        private double volume;

        public WorkingStep(string machineType, double volume)
        {
            this.machineType = machineType;
            this.volume = volume;
        }

        public string GetMachineType() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public double GetVolume() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }
    }
}
