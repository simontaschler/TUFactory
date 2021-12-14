using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class TurningMachine : Machine
    {
        private readonly double cuttingDepth;
        private readonly double cuttingSpeed;
        private readonly double feed;
        private double turnedVolume;

        public override string Type => "Drehmaschine";

        public TurningMachine(int id, int errorProbability, double cuttingSpeed, double cuttingDepth, double feed, int xCoordinate, int yCoordinate) : base(id, errorProbability, xCoordinate, yCoordinate)
        {
            this.cuttingSpeed = cuttingSpeed;
            this.cuttingDepth = cuttingDepth;
            this.feed = feed;
            metalRemovalRate = cuttingSpeed * cuttingDepth * feed * 1000;
        }

        public override double GetCalcMachineTime() => 
            Math.Ceiling(turnedVolume / metalRemovalRate);

        public override double GetInfluenceOnQuality() => 
            wear / 45;

        public override void SetMachinedVolume() => 
            turnedVolume = CurrentPart.GetNextMachiningVolume();
    }
}
