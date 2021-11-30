using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class Management
    {
        private List<Part> allParts;
        private List<Machine> brokenMachines;
        private List<Part> finishedParts;
        private List<Machine> machines;
        private List<Part> openParts;
        private QualityManagement qualityManagement;
        private List<Machine> workingMachines;

        public Management(QualityManagement qualityManagement)
        {
            this.qualityManagement = qualityManagement;
        }

        public void AddMachine(Machine machine)
        {
            machines.Add(machine);
            workingMachines.Add(machine);
        }

        public void Produce(int currentTime)
        {
            throw new NotImplementedException();
        }

        public void ReadOrders()
        {
            var lines = File.ReadAllLines(@"C:\Users\SimonT\Documents\Uni\WS21-22\Ingenieurinformatik 2\Basisprojekte\2\v0\TeileListe_neu.csv").Skip(1);
            allParts = new List<Part>();
            foreach (var line in lines) 
            {
                var fields = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var priority = int.Parse(fields[1]);

                var workSteps = new List<WorkingStep>();
                for (var i = 2; i < fields.Length; i += 2)
                    workSteps.Add(new WorkingStep(fields[i], double.Parse(fields[i + 1])));
                
                allParts.Add(new Part(workSteps, priority));
            }
        }

        public void SendToQualityCheck()
        {
            foreach (var finishedPart in finishedParts)
                qualityManagement.CheckQuality(finishedPart);
        }

        public void SimulatePossibleError(int currentTime)
        {
            var newlyBrokenMachines = new List<Machine>();
            foreach (var machine in workingMachines) 
            {
                if (machine.HasErrorOccured())
                {
                    newlyBrokenMachines.Add(machine);
                    Console.WriteLine($"Fehler bei {machine} aufgetreten");
                }
            }
            brokenMachines.AddRange(newlyBrokenMachines);

            foreach (var brokenMachine in brokenMachines) 
            { 
                if (!brokenMachine.GetInRepair()) 
                {
                    brokenMachine.SetInRepair(true);
                    brokenMachine.AddToEndTime(3);
                    Console.WriteLine($"Reparatur von {brokenMachine} beginnt");
                }
            }

            foreach (var newlyBrokenMachine in newlyBrokenMachines) 
                workingMachines.Remove(newlyBrokenMachine);

            newlyBrokenMachines = null; //um schneller von Garbage-Collection freigegeben zu werden

            var newlyRepairedMachines = new List<Machine>();
            foreach (var brokenMachine in brokenMachines)
            {
                if (currentTime >= brokenMachine.GetEndTime()) 
                {
                    brokenMachine.SetInUse(false);
                    brokenMachine.SetInRepair(false);
                    newlyRepairedMachines.Add(brokenMachine);
                    brokenMachine.Repair();
                    Console.WriteLine($"{brokenMachine} wurde repariert und ist wieder frei");
                }
            }
            workingMachines.AddRange(newlyRepairedMachines);

            foreach (var newlyRepairedMachine in newlyRepairedMachines)
                brokenMachines.Remove(newlyRepairedMachine);
        }

        public void WriteAllQualities() //evtl. Name ändern
        {
            foreach (var part in allParts)
                Console.WriteLine($"{part} Qualität: {part.GetQuality()}");
        }

        public void WriteStates() //GetStates()
        {
            foreach (var part in allParts)
                Console.WriteLine(part);
        }
    }
}
