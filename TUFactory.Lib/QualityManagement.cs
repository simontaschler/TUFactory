using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class QualityManagement
    {
        private double allowedQuality;
        private List<Part> badParts;
        private List<Part> goodParts;
        private int xCoordinate;
        private int yCoordinate;

        public QualityManagement(double allowedQuality, int xCoordinate, int yCoordinate)
        {
            this.allowedQuality = allowedQuality;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }

        public void CheckQuality(Part part) 
        {
            throw new NotImplementedException();
        }
    }
}
