using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class MillingMachine : Machine
    {
        private double cuttingDepth;
        private double cuttingWidth;
        private double feedingSpeed;
        private double millingVolume;

        public MillingMachine(int id, int errorProbability, double cuttingWidth, double cuttingDepth, double feedingSpeed, int xCoordinate, int yCoordinate) : base(id, errorProbability, xCoordinate, yCoordinate)
        {
            this.cuttingWidth = cuttingWidth;
            this.cuttingDepth = cuttingDepth;
            this.feedingSpeed = feedingSpeed;
            metalRemovalRate = cuttingDepth * cuttingWidth * feedingSpeed;
            type = "Fräsmaschine";
        }

        public override double GetCalcMachineTime()
        {
            return Math.Ceiling(millingVolume / metalRemovalRate);
        }

        public override double GetInfluenceOnQuality()
        {
            return wear / 50;
        }

        public override void SetMachinedVolume()
        {
            millingVolume = currentPart.GetNextMachiningVolume();
        }
    }
}
