using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> orbitingObjects;
            orbitingObjects = getOrbitsFromFile(@"input.txt");
            //Now I have a dictionary of each parents and its children
            //But I want a count of each child and its parents
            var orbitTotal = 0;
            foreach (string key in orbitingObjects.Keys)
            {
                orbitTotal += orbitCount(key, orbitingObjects);
            }
            Console.WriteLine(orbitTotal);
        }

        static Dictionary<string, List<string>> getOrbitsFromFile(string inputFilepath)
        {
            var orbitingObjects = File.ReadLines(inputFilepath)
                //Use .Select to apply the same method to each element
                .Select(x => x.Split(')'))
                .GroupBy(x => x[0], y => y[1])
                .ToDictionary(x => x.Key, y => y.ToList());
            return orbitingObjects;
        }

        static int orbitCount(string name, Dictionary<string, List<string>> orbits)
        {
            var count = 0;
            if (orbits.Keys.Contains(name))
            {
                foreach (string item in orbits[name])
                {
                    //check if child has orbits beneath it
                    count += 1 + orbitCount(item, orbits);
                }
            }
            return count;
        }

    }
    
}