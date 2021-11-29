using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class TurningMachine : Machine
    {
        private double cuttingDepth;
        private double cuttingSpeed;
        private double feed;
        private double turnedVolume;

        public TurningMachine(int id, int errorProbability, double cuttingSpeed, double cuttingDepth, double feed, int xCoordinate, int yCoordinate) : base(id, errorProbability, xCoordinate, yCoordinate)
        {
            this.cuttingSpeed = cuttingSpeed;
            this.cuttingDepth = cuttingDepth;
            this.feed = feed;
        }

        public override double GetCalcMachineTime()
        {
            throw new NotImplementedException();
        }

        public override double GetInfluenceOnQuality()
        {
            throw new NotImplementedException();
        }

        public override void SetMachinedVolume()
        {
            throw new NotImplementedException();
        }
    }
}
