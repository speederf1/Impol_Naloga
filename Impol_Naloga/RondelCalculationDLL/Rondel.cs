using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RondelCalculationDLL
{
    public class Rondel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"X:[{X.ToString()}], Y:[{Y.ToString()}]";
        }
    }
}
