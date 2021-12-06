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
        private readonly List<Machine> brokenMachines;
        private readonly List<Part> finishedParts;
        private readonly List<Machine> machines;
        private readonly List<Part> openParts;
        private readonly QualityManagement qualityManagement;
        private readonly List<Machine> workingMachines;

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

        public void Produce(int currentTime)
        {
            //entfernen von kürzlich fertiggestellten Teilen
            var newlyFinishedparts = openParts.Where(q => q.GetNumberOfOpenOperations() == 0);
            finishedParts.AddRange(newlyFinishedparts);
            foreach (var openPart in newlyFinishedparts) 
            { 
                openPart.SetState(3);
                openParts.Remove(openPart);
            }

            foreach (var openPart in openParts)
            {
                if (workingMachines.FirstOrDefault(q => !q.GetInUse() && openPart.GetNextMachineType() == q.GetMachineType()) is Machine workingMachine) 
                //Teil wird in freier Maschine bearbeitet
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

            //Bearbeitungsschritt eines Teils fertig
            foreach (var workingMachine in workingMachines.Where(q => currentTime >= q.GetEndTime())) 
            { 
                workingMachine.SetInUse(false);
                if (workingMachine.GetCurrentPart() is Part currentPart) 
                {
                    currentPart.SetPartFree();
                    currentPart.DeleteMachiningStep();
                    workingMachine.SetCurrentPart(null);
                    Console.WriteLine($"{workingMachine} ist wieder frei und bereit zum Bearbeiten neuer Teile");
                }
            }
        }

        public void ReadOrders(IEnumerable<string> lines)
        {
            allParts = new List<Part>();
            foreach (var line in lines.Skip(1)) 
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

        public void SendToQualityCheck() => 
            finishedParts.ForEach(q => qualityManagement.CheckQuality(q));

        public void SimulatePossibleError(int currentTime)
        {
            //Auftreten von Fehlern simulieren
            var newlyBrokenMachines = workingMachines.Where(q => q.HasErrorOccured());
            brokenMachines.AddRange(newlyBrokenMachines);
            foreach (var newlyBrokenMachine in newlyBrokenMachines) 
            {
                Console.WriteLine($"Fehler bei {newlyBrokenMachine} aufgetreten");
                workingMachines.Remove(newlyBrokenMachine);
            }

            var newlyRepairedMachines = new List<Machine>();
            foreach (var brokenMachine in brokenMachines)
            {
                //beschädigte Maschinen in Reparatur schicken
                if (!brokenMachine.GetInRepair())
                {
                    brokenMachine.SetInRepair(true);
                    brokenMachine.AddToEndTime(3);
                    Console.WriteLine($"Reparatur von {brokenMachine} beginnt");
                }

                //kürzlich reparierte Maschinen
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

        public void WriteAllQualities() => 
            allParts.ForEach(q => Console.WriteLine($"{q} Qualität: {q.GetQuality()}"));

        public void WriteStates() => //GetStates()
            allParts.ForEach(q => Console.WriteLine(q));
    }
}
