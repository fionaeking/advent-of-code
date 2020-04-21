using System;
using System.Collections.Generic;
using System.IO;

// Monitoring Station

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get asteroid coordinates
            var astrList = readInInput();
            var maxAsteroidCount = getMaxAsteroidCount(astrList);
            Console.WriteLine(maxAsteroidCount);
        }

        static List<Tuple<int, int>> readInInput()
        {
            var astrList = new List<Tuple<int, int>>();
            var inputFile = File.ReadLines(Constants.INPUT_FILENAME);
            int lineCount = 0;
            foreach (var line in inputFile)
            {
                int charCount = 0;
                foreach (var ch in line)
                {
                    if(ch=='#')
                    {
                        astrList.Add(new Tuple<int, int>(charCount, lineCount));
                    }
                    charCount++;
                }
                lineCount++;
            }
            return astrList;
        }

        static int getMaxAsteroidCount(List<Tuple<int, int>> astrList){
            var maxAsteroidCount = 0;
            //Tuple<int, int> bestPos = new Tuple<int, int>(0, 0);
            foreach (var monitorStation in astrList)
            {
                var astrAngleDict = new Dictionary<double, Tuple<int, int>>();
                foreach (var asteroid in astrList)
                {
                    if (asteroid!=monitorStation)
                    {
                        var angle = calculateAngle(monitorStation, asteroid);
                        astrAngleDict[angle] = asteroid;
                    }
                }  
                maxAsteroidCount = Math.Max(astrAngleDict.Count, maxAsteroidCount);
                //bestPos = monitorStation;      
            }
            return maxAsteroidCount;
        }

        static double calculateAngle(Tuple<int, int> a, Tuple<int, int> b)
        {
            int nom = b.Item1 - a.Item1;
            int denom = b.Item2 - a.Item2;
            return Math.Atan2(nom, denom);
        }
    }
}
