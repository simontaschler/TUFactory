using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public abstract class Machine
    {
        private double startTimeInUse;  //time sonst als int?
        private double endTimeInUse;  //time sonst als int?
        private int errorProbability = 5;
        private bool inRepair;
        private bool inUse;
        private int id;
        private int xCoordinate;
        private int yCoordinate;

        protected Part currentPart;
        protected string type;      //überflüssig, Type durch erbende Klassen bereits definiert
        protected double metalRemovalRate;
        protected double wear;

        public Machine(int id, int errorProbability, int xCoordinate, int yCoordinate)
        {
            this.id = id;
            this.errorProbability = errorProbability;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;

            //Testszenario
            //errorProbability = 0;
        }

        public abstract double GetCalcMachineTime(); //Name zu ändern, kein GetCalc
        public abstract void SetMachinedVolume(); //Name zu ändern, kein Set ohne Parameter

        public void AddToEndTime(int endTime) 
        {
            endTimeInUse += endTime;
        }

        public Part GetCurrentPart() //durch Property zu ersetzen
        {
            return currentPart;
        }

        public double GetEndTime() //durch Property zu ersetzen
        {
            return endTimeInUse;
        }

        public virtual double GetInfluenceOnQuality()
        {
            return 0;
        }

        public bool GetInRepair() //durch Property zu ersetzen
        {
            return inRepair;
        }

        public bool GetInUse() //durch Property zu ersetzen
        {
            return inUse;
        }

        public string GetMachineType() //durch Property zu ersetzen
        {
            return type;
        }

        public bool HasErrorOccured() //PossibleError()
        {
            return new Random().Next(0, 99) <= errorProbability || wear >= .75;
        }

        public void Repair()
        {
            wear = 0;
        }

        public void SetCurrentPart(Part value) //durch Property ersetzen
        {
            currentPart = value;
        }

        public void SetInRepair(bool value) //durch Property ersetzen, mehr Sinn als private das von Repair aufgerufen wird
        {
            inRepair = value;
        }

        public void SetInUse(bool value) //durch Property ersetzen, evtl. auch als private
        {
            inUse = value;
        }

        public void SetTimesAndCalcWear(double currentTime, double endTime) //Zeit int & double gemischt
        {
            startTimeInUse = currentTime;
            endTimeInUse = endTime;
            wear += (endTime - currentTime) / 20;
        }

        public override string ToString() =>
            $"{GetMachineType()} {id}"; //Methode statt Feld für Umstellung auf Properties
    }
}
