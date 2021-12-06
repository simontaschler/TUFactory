using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class GrindingMachine : Machine
    {
        private readonly double cuttingSpeed;
        private double grindingVolume;
        private readonly double grindingWidth;
        private readonly double infeed;
        private readonly double speedRelation;

        public GrindingMachine(int id, int errorProbability, double infeed, double grindingWidth, double cuttingSpeed, double speedRelation, int xCoordinate, int yCoordinate) : base(id, errorProbability, xCoordinate, yCoordinate)
        {
            this.infeed = infeed;
            this.grindingWidth = grindingWidth;
            this.cuttingSpeed = cuttingSpeed;
            this.speedRelation = speedRelation;
            metalRemovalRate = infeed * grindingWidth * cuttingSpeed / speedRelation;
            type = "Schleifmaschine";
        }

        public override double GetCalcMachineTime() => 
            Math.Ceiling(grindingVolume / metalRemovalRate);

        public override void SetMachinedVolume() => 
            grindingVolume = currentPart.GetNextMachiningVolume();
    }
}
