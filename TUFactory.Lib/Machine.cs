using System;

namespace TUFactory.Lib
{
    public abstract class Machine
    {
        private readonly int errorProbability = 0;
        private readonly int id;
        private readonly int xCoordinate;
        private readonly int yCoordinate;

        protected double metalRemovalRate;
        protected double wear;

        public Part CurrentPart { get; set; }
        public bool InRepair { get; set; }
        public bool InUse { get; set; }
        public double StartTimeInUse { get; private set; } //time sonst als int?
        public double EndTimeInUse { get; private set; }

        public abstract string Type { get; }

        public Machine(int id, int errorProbability, int xCoordinate, int yCoordinate)
        {
            this.id = id;
            this.errorProbability = errorProbability;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;

            //Testszenario
            //errorProbability = 0;
        }

        public abstract double CalcMachineTime(); //Name zu ändern, kein GetCalc
        //Methode erhöht Fehlerpotenzial massiv, da Aufruf erforderlich ist, um bei CalcMachineTime richtige Werte zu erhalten
        //public abstract void SetMachinedVolume(); //Name zu ändern, kein Set ohne Parameter

        public void AddToEndTime(int endTime) =>
            EndTimeInUse += endTime;

        public virtual double GetInfluenceOnQuality() =>
            0;

        public bool HasErrorOccured() //PossibleError()
        {
            System.Threading.Thread.Sleep(1);
            return new Random().Next(1, 100) <= errorProbability || wear >= .75;
        }

        public void Repair() =>
            wear = 0;

        public void SetTimesAndCalcWear(double currentTime, double endTime) //Zeit int & double gemischt
        {
            StartTimeInUse = currentTime;
            EndTimeInUse = endTime;
            wear += (endTime - currentTime) / 20;
        }

        public override string ToString() =>
            $"{Type} {id}";
    }
}
