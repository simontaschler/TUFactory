﻿using System.Collections.Generic;
using System.Linq;

namespace TUFactory.Lib
{
    public enum State : int
    {
        WorkPiece = 1,
        WorkInProgress = 2,
        Concluded = 3,
        QualityOk = 4,
        QualityNotOk = 5
    }

    //Methode ExecuteNextWorkStep hinzufügen um Fehleranfälligkeit zu senken
    public class Part
    {
        private readonly int currentXPosition;
        private readonly int currentYPosition;
        private readonly int priority;
        private readonly List<WorkingStep> workInstructions;

        private static int nextID = 1;

        public double Quality { get; set; }
        public Machine CurrentMachine { private get; set; }
        public State State { get; set; } //get nur für Unit-Test
        public int ID { get; }

        public Part(List<WorkingStep> workInstructions, int priority)
        {
            this.priority = priority;
            this.workInstructions = workInstructions;
            ID = nextID++;
            State = State.WorkPiece;
            Quality = 1;
        }

        //Bündeln der Aufrufe um Fehlerpotenzial zu minimieren
        public void ExecuteNextWorkstep(int currentTime, Machine nextMachine)
        {
            nextMachine.InUse = true;
            nextMachine.CurrentPart = this;
            nextMachine.SetTimesAndCalcWear(currentTime, currentTime + nextMachine.CalcMachineTime());
            State = State.WorkInProgress;
            CurrentMachine = nextMachine;
            Quality -= Quality * nextMachine.GetInfluenceOnQuality();
        }

        public void DeleteMachiningStep()
        {
            if (workInstructions.Count > 0)
                workInstructions.RemoveAt(0);
        }

        public string GetNextMachineType() =>
            workInstructions.FirstOrDefault()?.MachineType;

        public double GetNextMachiningVolume() =>
            workInstructions.FirstOrDefault()?.Volume ?? 0;

        public int GetNumberOfOpenOperations() =>
            workInstructions.Count();

        public void SetPartFree() =>
            CurrentMachine = null;

        public override string ToString() =>
            $"Part {ID} state: {(int)State}";
    }
}
