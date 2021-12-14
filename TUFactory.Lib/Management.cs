﻿using System;
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
        private List<Part> openParts;
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
            //Bearbeitungsschritt eines Teils fertig
            foreach (var workingMachine in workingMachines.Where(q => currentTime >= q.EndTimeInUse))
            {
                workingMachine.InUse = false;
                if (workingMachine.CurrentPart is Part currentPart)
                {
                    currentPart.SetPartFree();
                    currentPart.DeleteMachiningStep();
                    workingMachine.CurrentPart = null;
                    Console.WriteLine($"{workingMachine} ist wieder frei und bereit zum Bearbeiten neuer Teile");
                }
            }

            var newlyFinishedparts = new List<Part>();
            foreach (var openPart in openParts) 
            {
                //Teil fertig mit allen Bearbeitungsschritten
                if (openPart.GetNumberOfOpenOperations() == 0)
                {
                    openPart.State = State.Concluded;
                    newlyFinishedparts.Add(openPart);
                }
                //Teil wird in freier Maschine bearbeitet
                else if (workingMachines.FirstOrDefault(q => !q.InUse && openPart.GetNextMachineType() == q.Type) is Machine workingMachine)
                {
                    openPart.ExecuteNextWorkstep(currentTime, workingMachine);
                    Console.WriteLine($"{openPart} wird in {workingMachine} bearbeitet");
                }
            }
            finishedParts.AddRange(newlyFinishedparts);
            newlyFinishedparts.ForEach(q => openParts.Remove(q));
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
            }
            openParts = new List<Part>(allParts);
        }

        public void SendToQualityCheck() => 
            finishedParts.ForEach(q => qualityManagement.CheckQuality(q));

        public void SimulatePossibleError(int currentTime)
        {
            //Auftreten von Fehlern simulieren
            var newlyBrokenMachines = workingMachines.Where(q => q.HasErrorOccured()).ToList();
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
                if (!brokenMachine.InRepair)
                {
                    brokenMachine.InRepair = true;
                    brokenMachine.AddToEndTime(3);
                    Console.WriteLine($"Reparatur von {brokenMachine} beginnt");
                }

                //kürzlich reparierte Maschinen
                if (currentTime >= brokenMachine.EndTimeInUse)
                {
                    brokenMachine.InUse = false;
                    brokenMachine.InRepair = false;
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
            allParts.ForEach(q => Console.WriteLine($"{q} Qualität: {q.Quality:P4}"));

        public void WriteStates() => //GetStates()
            allParts.ForEach(q => Console.WriteLine(q));

        //nur für UnitTest
        public List<Part> AllParts =>
            allParts;
    }
}
