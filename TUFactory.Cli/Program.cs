using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUFactory.Lib;

namespace TUFactory.Cli
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var orderFile = @"C:\Users\SimonT\Documents\Uni\WS21-22\Ingenieurinformatik 2\Basisprojekte\2\v0\TeileListe_neu.csv";
            var qualityManagement = new QualityManagement(.95, 0, 0);
            var management = new Management(qualityManagement);
            management.AddMachine(new TurningMachine(1, 0, 75, .5, .1, 10, 10));
            management.AddMachine(new MillingMachine(2, 0, 25, 5, 20, 0, 10));
            management.AddMachine(new GrindingMachine(3, 0, .02, 50, 30, 80, 20, 0));

            management.ReadOrders(orderFile);

            for (var timeStep = 0; timeStep <= 30; timeStep++) 
            {
                Console.WriteLine($"--------------------------------TIME {timeStep}--------------------------------");
                management.Produce(timeStep);
                management.SendToQualityCheck();
                management.SimulatePossibleError(timeStep);
                management.WriteStates();
            }

            Console.WriteLine("--------------------------------QUALITIES--------------------------------");
            management.WriteAllQualities();

            Console.ReadLine();
        }
    }
}
