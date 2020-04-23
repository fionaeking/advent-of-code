using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Utilities
{
    public static double calculateAngle(Tuple<int, int> a, Tuple<int, int> b)
    {
        int nom = a.Item1 - b.Item1;
        int denom = b.Item2 - a.Item2;
        // +180 rather than +360 as y coordinates are labelled in reverse
        return ( Math.Atan2(nom, denom) * (180 / Math.PI) + 180) % 360;
    }

    public static double getDistance(Tuple<int, int> pointOne, Tuple<int, int> pointTwo)
    {
         return Math.Sqrt(Math.Pow((pointTwo.Item1 - pointOne.Item1), 2) + Math.Pow((pointTwo.Item2 - pointOne.Item2), 2));
    }
}
