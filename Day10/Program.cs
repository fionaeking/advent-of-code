using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;

// Monitoring Station

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var astrList = readInInput(); // Get asteroids as list of coordinates
            Tuple<int, int> bestPos = getBestPosition(astrList);
            var currAsteroidCount = 0;
            var prevAsteroidCount = 0;
            List<KeyValuePair<double, Tuple<int, int>>> listAnglesOrdered = new List<KeyValuePair<double, Tuple<int, int>>>();
            while (currAsteroidCount<=Constants.VAPOURISED_ASTEROID_NUM)
            {
                var astrAngleDict = new Dictionary<double, Tuple<int, int>>();
                foreach (var asteroid in astrList)
                {
                    if (asteroid!=bestPos)
                    {
                        var angle = Utilities.calculateAngle(bestPos, asteroid);
                        // If angle already exists as dictionary key, check if this point is closer
                        astrAngleDict[angle] = astrAngleDict.ContainsKey(angle) ? 
                                                closerPoint(bestPos, asteroid, astrAngleDict[angle]) : asteroid;
                    }
                }
                listAnglesOrdered = astrAngleDict.OrderBy(key => key.Key).ToList(); // Sort angles in order
                prevAsteroidCount = currAsteroidCount;
                currAsteroidCount += listAnglesOrdered.Count;
            }
            Console.WriteLine($"200th asteroid at: {listAnglesOrdered[200 - prevAsteroidCount - 1].Value}");
        }

        static Tuple<int, int> closerPoint(Tuple<int, int> pointOrigin, Tuple<int, int> pointOne, Tuple<int, int> pointTwo)
        {
            var distOne = Utilities.getDistance(pointOrigin, pointOne);
            var distTwo = Utilities.getDistance(pointOrigin, pointTwo);
            return (distOne < distTwo) ? pointOne : pointTwo;
        }

        static List<Tuple<int, int>> readInInput()
        {
            var astrList = new List<Tuple<int, int>>();
            var inputFile = File.ReadLines(Constants.INPUT_FILENAME);
            int lineCount = 0;
            foreach (var line in inputFile)
            {
                for (int charCount = 0; charCount<line.Length; charCount++)
                {
                    if(line[charCount]==Constants.ASTEROID)
                    {
                        astrList.Add(new Tuple<int, int>(charCount, lineCount));
                    }
                    charCount++;
                }
                lineCount++;
            }
            return astrList;
        }

        static Tuple<int, int> getBestPosition(List<Tuple<int, int>> astrList){
            var maxAsteroidCount = 0;
            Tuple<int, int> bestPos = new Tuple<int, int>(0, 0);
            foreach (var monitorStation in astrList)
            {
                var astrAngleDict = new Dictionary<double, Tuple<int, int>>();
                foreach (var asteroid in astrList)
                {
                    if (asteroid!=monitorStation)
                    {
                        var angle = Utilities.calculateAngle(monitorStation, asteroid);
                        astrAngleDict[angle] = asteroid;
                    }
                } 
                if (astrAngleDict.Count > maxAsteroidCount)
                {
                    maxAsteroidCount = astrAngleDict.Count;
                    bestPos = monitorStation;
                }
            }
            Console.WriteLine("Max asteroid count: " + maxAsteroidCount);
            Console.WriteLine("Best position: " + bestPos);
            return bestPos;
        }

    }
}
