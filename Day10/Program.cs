using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Monitoring Station

namespace Day10  //1904 was wrong - too low
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get asteroid coordinates
            var astrList = readInInput();
            var maxAsteroidCount = getMaxAsteroidCount(astrList);
            Tuple<int, int> bestPos = new Tuple<int, int>(20, 21);
            var asteroidCount = 0;
            while (asteroidCount<=200)
            {
                var astrAngleDict = new Dictionary<double, Tuple<int, int>>();
                foreach (var asteroid in astrList)
                {
                    if (asteroid!=bestPos)
                    {
                        var angle = calculateAngle(bestPos, asteroid);
                        //if angle exists, check if the distance is larger
                        if (astrAngleDict.ContainsKey(angle))
                        {
                            astrAngleDict[angle] = closerPoint(bestPos, asteroid, astrAngleDict[angle]);
                        }
                        else
                        {
                            astrAngleDict[angle] = asteroid;
                        }
                    }
                }
                var l = astrAngleDict.OrderBy(key => key.Key);
                var l2 = l.ToList(); //(valueItem) => valueItem.Value);
                //var dic = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
                if ((asteroidCount+l2.Count)>200)
                {
                    Console.WriteLine("yes");
                    Console.WriteLine(l2[0]);
                    Console.WriteLine(l2[200 - asteroidCount - 1]);
                    break;
                }
                else
                {
                    Console.WriteLine("non");
                    asteroidCount += l2.Count;
                    foreach (var val in astrAngleDict.Values)
                    {
                        astrList.Remove(val);
                    }
                }


            }
            

                //bestPos = monitorStation;      
        }

        static double getDistance(Tuple<int, int> pointOne, Tuple<int, int> pointTwo)
        {
             return Math.Sqrt(Math.Pow((pointTwo.Item1 - pointOne.Item1), 2) + Math.Pow((pointTwo.Item2 - pointOne.Item2), 2));
        }

        static Tuple<int, int> closerPoint(Tuple<int, int> pointOrigin, Tuple<int, int> pointOne, Tuple<int, int> pointTwo)
        {
            var distOne = getDistance(pointOrigin, pointOne);
            var distTwo = getDistance(pointOrigin, pointTwo);
            return (distOne < distTwo) ? pointOne : pointTwo;
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
            Tuple<int, int> bestPos = new Tuple<int, int>(0, 0);
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
                //maxAsteroidCount = Math.Max(astrAngleDict.Count, maxAsteroidCount);
                if (astrAngleDict.Count > maxAsteroidCount)
                {
                    maxAsteroidCount = astrAngleDict.Count;
                    bestPos = monitorStation;
                }
            }
            Console.WriteLine("Max asteroid count: " + maxAsteroidCount);
            Console.WriteLine(bestPos);
            return maxAsteroidCount;
        }

        static double calculateAngle(Tuple<int, int> a, Tuple<int, int> b)
        {
            int nom = a.Item1 - b.Item1;
            int denom = b.Item2 - a.Item2;
            var angleToReturn = ( Math.Atan2(nom, denom) * (180 / Math.PI) + 180) % 360;
           // var angleToReturn = Math.Atan2(nom, denom) * 180/Math.PI;
            /*if (angleToReturn < 0)
            {
                angleToReturn += 360;
            }*/
            return angleToReturn;
        }
    }
}
