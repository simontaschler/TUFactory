using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    //Methode ExecuteNextWorkStep hinzufügen um Fehleranfälligkeit zu senken
    public class Part
    {
        private Machine currentMachine;
        private int currentXPosition;
        private int currentYPosition;
        private int id;
        private int priority;
        private double quality;
        private int state;      //1, 2, 3, 4, 5 => mit enum zu ersetzen
        private List<WorkingStep> workInstructions;

        private static int nextID = 1;

        public Part(List<WorkingStep> workInstructions, int priority) 
        {
            this.priority = priority;
            this.workInstructions = workInstructions;
            id = nextID++;
        }

        public void DeleteMachiningStep() 
        {
            if (workInstructions.Count > 0)
                workInstructions.RemoveAt(0);
        }

        public string GetNextMachineType() 
        {
            return workInstructions.First().GetMachineType();
        }

        public double GetNextMachiningVolume()
        {
            return workInstructions.First().GetVolume();
        }

        public int GetNumberOfOpenOperations()
        {
            return workInstructions.Count();
        }

        public double GetQuality()  //durch Property zu ersetzen
        {
            return quality;
        }

        public void SetCurrentMachine(Machine value) //durch Property zu ersetzen
        {
            currentMachine = value;
        }

        public void SetPartFree()
        {
            currentMachine = null;
        }

        public void SetQuality(double value) //durch Property zu ersetzen
        {
            quality = value;
        }

        public void SetState(int value) //durch Property zu ersetzen
        {
            state = value;
        }

        public override string ToString() =>
            $"Part {id} state: {state}";
    }
}
