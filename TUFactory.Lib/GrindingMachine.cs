using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class GrindingMachine : Machine
    {
        private double cuttingSpeed;
        private double grindingVolume;
        private double grindingWidth;
        private double infeed;
        private double speedRelation;

        public GrindingMachine(int id, int errorProbability, double infeed,/* double grindingWidth,*/ double cuttingSpeed, double speedRelation, int xCoordinate, int yCoordinate) : base(id, errorProbability, xCoordinate, yCoordinate)
        {
            this.infeed = infeed;
            grindingWidth = 50; //laut Angabe konst. 50 mm
            this.cuttingSpeed = cuttingSpeed;
            this.speedRelation = speedRelation;
        }

        public override double GetCalcMachineTime()
        {
            return Math.Ceiling(grindingVolume / metalRemovalRate);
        }

        public override void SetMachinedVolume()
        {
            throw new NotImplementedException();
        }
    }
}
