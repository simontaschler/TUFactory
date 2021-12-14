using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class MillingMachine : Machine
    {
        private readonly double cuttingDepth;
        private readonly double cuttingWidth;
        private readonly double feedingSpeed;
        private double millingVolume;

        public override string Type => "Fräsmaschine";

        public MillingMachine(int id, int errorProbability, double cuttingWidth, double cuttingDepth, double feedingSpeed, int xCoordinate, int yCoordinate) : base(id, errorProbability, xCoordinate, yCoordinate)
        {
            this.cuttingWidth = cuttingWidth;
            this.cuttingDepth = cuttingDepth;
            this.feedingSpeed = feedingSpeed;
            metalRemovalRate = cuttingDepth * cuttingWidth * feedingSpeed;
        }

        public override double GetCalcMachineTime() => 
            Math.Ceiling(millingVolume / metalRemovalRate);

        public override double GetInfluenceOnQuality() => 
            wear / 50;

        public override void SetMachinedVolume() => 
            millingVolume = CurrentPart.GetNextMachiningVolume();
    }
}
