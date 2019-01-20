using System;
using System.Collections.Generic;

namespace RondelCalculationDLL
{
    public static class RondelCalculation
    {
        // reference: https://www.engineeringtoolbox.com/circles-within-rectangle-d_1905.html and https://math.stackexchange.com/questions/2548513/maximum-number-of-circle-packing-into-a-rectangle
        public static List<Rondel> Calculate(double width, double length, double r, double minDistanceBetween, double minDistanceFromEdges)
        {
            List<Rondel> rondels = new List<Rondel>();

            // first rondel position
            Rondel firstRondel = new Rondel { X = r + minDistanceFromEdges, Y = r + minDistanceFromEdges };
            Rondel tempRondel = firstRondel;
            int indexUp = 0;
            
            // if rectangle is not big enough for rondel
            if (firstRondel.X + r + minDistanceFromEdges > length || firstRondel.Y + r + minDistanceFromEdges > width)
                return new List<Rondel>();

            // add rondel
            rondels.Add(firstRondel);

            while (true)
            {
                // go right
                while (true)
                {
                    // new x = last x + distance between + 2 * r, new y = last y 
                    Rondel tempRight = new Rondel { X = tempRondel.X + minDistanceBetween + 2 * r, Y = tempRondel.Y };

                    // if rondel does not fit into rectangle
                    if (tempRight.X + r + minDistanceFromEdges > length)
                        break;

                    // add rondel
                    rondels.Add(tempRight);
                    tempRondel = tempRight;
                }

                // go up
                indexUp++;
                tempRondel = firstRondel;
                Rondel tempUp;

                // if new row is even -> new x = first x, new y = first y + (2 * r + distance between) * sqrt(3) / 2 * row number
                if (indexUp % 2 == 0)
                    tempUp = new Rondel { X = tempRondel.X, Y = tempRondel.Y + ((2 * r + minDistanceBetween) * Math.Sqrt(3)) / 2 * indexUp };
                // if new row is odd -> new x = first x + distance between + r, new y = first y + (2 * r + distance between) * sqrt(3) / 2 * row number
                else
                tempUp = new Rondel { X = tempRondel.X + minDistanceBetween + r, Y = tempRondel.Y + ((2 * r + minDistanceBetween) * Math.Sqrt(3)) / 2 * indexUp };

                // if rondel does not fit into rectangle
                if (tempUp.X + r + minDistanceFromEdges > length || tempUp.Y + r + minDistanceFromEdges > width)
                    break;

                // add rondel
                rondels.Add(tempUp);
                tempRondel = tempUp;
            }

            return rondels;
        }
    }
}
