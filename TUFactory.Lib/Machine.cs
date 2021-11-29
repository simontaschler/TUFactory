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
        private int errorProbability;
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
        }

        public abstract double GetCalcMachineTime(); //Name zu ändern, kein GetCalc
        public abstract void SetMachinedVolume(); //Name zu ändern, kein Set ohne Parameter

        public void AddToEndTime(int endTime) 
        {
            throw new NotImplementedException();
        }

        public Part GetCurrentPart() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public double GetEndTime() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public virtual double GetInfluenceOnQuality()
        {
            throw new NotImplementedException();
        }

        public bool GetInRepair() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public bool GetInUse() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public string GetMachineType() //durch Property zu ersetzen
        {
            throw new NotImplementedException();
        }

        public bool PossibleError() //Property? oder Name ändern
        {
            throw new NotImplementedException();
        }

        public void Repair()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentPart(Part value) //durch Property ersetzen
        {
            throw new NotImplementedException();
        }

        public void SetInRepair(bool value) //durch Property ersetzen, mehr Sinn als private das von Repair aufgerufen wird
        {
            throw new NotImplementedException();
        }

        public void SetInUse(bool value) //durch Property ersetzen, evtl. auch als private
        {
            throw new NotImplementedException();
        }

        public void SetTimesAndCalcWear(double currentTime, double endTime) //Zeit int & double gemischt
        {
            throw new NotImplementedException();
        }

        public override string ToString() => 
            base.ToString();
    }
}
