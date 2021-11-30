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
            brokenMachines = new List<Machine>();
            machines = new List<Machine>();
            workingMachines = new List<Machine>();
            openParts = new List<Part>();
            finishedParts = new List<Part>();
        }

        public void AddMachine(Machine machine)
        {
            machines.Add(machine);
            workingMachines.Add(machine);
        }

        public void Produce(int currentTime)        //Schleifen zusammenfassen, mit Linq massiv zu kürzen
        {
            var newlyFinishedparts = new List<Part>();
            foreach (var openPart in openParts) 
            { 
                if (openPart.GetNumberOfOpenOperations() == 0) 
                {
                    openPart.SetState(3);
                    newlyFinishedparts.Add(openPart);
                }
            }
            finishedParts.AddRange(newlyFinishedparts);

            foreach (var newlyFinishedPart in newlyFinishedparts)
                openParts.Remove(newlyFinishedPart);

            foreach (var openPart in openParts)
            {
                foreach (var workingMachine in workingMachines) 
                { 
                    if (!workingMachine.GetInUse() && openPart.GetNextMachineType() == workingMachine.GetMachineType()) 
                    {
                        workingMachine.SetInUse(true);
                        workingMachine.SetCurrentPart(openPart);
                        workingMachine.SetMachinedVolume();
                        workingMachine.SetTimesAndCalcWear(currentTime, currentTime + workingMachine.GetCalcMachineTime());
                        openPart.SetState(2);
                        openPart.SetCurrentMachine(workingMachine);
                        openPart.SetQuality(openPart.GetQuality() - openPart.GetQuality() * workingMachine.GetInfluenceOnQuality());

                        Console.WriteLine($"{openPart} wird in {workingMachine} bearbeitet");
                        break;
                    }
                }
            }

            foreach (var workingMachine in workingMachines) 
            { 
                if (!workingMachine.GetInRepair() && currentTime >= workingMachine.GetEndTime()) //Abfangen von Maschinenausfällen
                {
                    workingMachine.SetInUse(false);
                    if (workingMachine.GetCurrentPart() != null) 
                    {
                        var currentPart = workingMachine.GetCurrentPart();
                        currentPart.SetPartFree();
                        currentPart.DeleteMachiningStep();
                        workingMachine.SetCurrentPart(null);
                        Console.WriteLine($"{workingMachine} ist wieder frei und bereit zum Bearbeiten neuer Teile");
                    }
                }
            }
        }

        public void ReadOrders(string file)
        {
            var lines = File.ReadAllLines(file).Skip(1);
            allParts = new List<Part>();
            foreach (var line in lines) 
            {
                var fields = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var priority = int.Parse(fields[1]);

                var workSteps = new List<WorkingStep>();
                for (var i = 2; i < fields.Length; i += 2)
                    workSteps.Add(new WorkingStep(fields[i], double.Parse(fields[i + 1])));

                var part = new Part(workSteps, priority);
                allParts.Add(part);
                openParts.Add(part);
            }
        }

        public void SendToQualityCheck()
        {
            foreach (var finishedPart in finishedParts)
                qualityManagement.CheckQuality(finishedPart);
        }

        public void SimulatePossibleError(int currentTime) //Schleifen zusammenfassen, mit Linq massiv zu kürzen
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
