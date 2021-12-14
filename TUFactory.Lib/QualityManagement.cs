using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUFactory.Lib
{
    public class QualityManagement
    {
        private readonly double allowedQuality;
        private readonly List<Part> badParts;
        private readonly List<Part> goodParts;
        private readonly int xCoordinate;
        private readonly int yCoordinate;

        public QualityManagement(double allowedQuality, int xCoordinate, int yCoordinate)
        {
            this.allowedQuality = allowedQuality;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            goodParts = new List<Part>();
            badParts = new List<Part>();
        }

        public void CheckQuality(Part part) 
        {
            if (part.Quality <= allowedQuality) 
            {
                badParts.Add(part);
                part.State = State.QualityNotOk;
            }
            else
            {
                goodParts.Add(part);
                part.State = State.QualityOk;
            }
        }
    }
}
