using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class Part
    {
        private Machine currentMachine;
        private int currentXPosition;
        private int currentYPosition;
        private int id;
        private int priority;
        private double quality;
        private int state;
        private List<WorkingStep> workInstructions;

        private static int nextID = 1;

        public Part(List<WorkingStep> workInstructions, int priority) 
        {
            this.priority = priority;
            this.workInstructions = workInstructions;
            id = nextID++;
        }

        public void DeleteMachiningStep() 
        { }

        public string GetNextMachineType() 
        {
            throw new NotImplementedException();
        }

        public double GetNextMachiningVolume()
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfOpenOperations()
        {
            throw new NotImplementedException();
        }

        public double GetQuality()  //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public void SetCurrentMachine(Machine value) //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public void SetPartFree()
        {
            throw new NotImplementedException();
        }

        public void SetQuality(double value) //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public void SetState(int value) //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public override string ToString() => 
            base.ToString();
    }
}
