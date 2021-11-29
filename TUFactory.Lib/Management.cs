using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void GetStates() //Name ändern, keine void Getter
        {
            throw new NotImplementedException();
        }

        public void Produce(int currentTime)
        {
            throw new NotImplementedException();
        }

        public void ReadOrders()
        {
            throw new NotImplementedException();
        }

        public void SendToQualityCheck()
        {
            throw new NotImplementedException();
        }

        public void SimulatePossibleError(int currentTime)
        {
            throw new NotImplementedException();
        }

        public void WriteAllQualities() //evtl. Name ändern
        {
            throw new NotImplementedException();
        }
    }
}
