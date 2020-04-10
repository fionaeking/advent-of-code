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

            //Part 2
            //Find starting objects for SAN and YOU
            //Minimum distance will be at first point parent is the same
            /*foreach (string key in orbitingObjects.Keys)
            {
                Console.WriteLine("Key: " + key);
                foreach (string val in orbitingObjects[key])
                {
                    Console.WriteLine(val);
                }

            }*/
            findCommonParent("SAN", "YOU", orbitingObjects);
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

        static void findCommonParent(string one, string two, Dictionary<string, List<string>> orbits)
        {
            var firstvalue = one;
            var secondvalue = two;

            var firstParent = findParent(firstvalue, orbits);
            var secondParent = findParent(secondvalue, orbits);
            var countOne = 0;
            while(firstParent!=secondParent)
            {
                var countTwo = 0;
                while (secondParent!="COM")
                {
                    if (firstParent == secondParent)
                    {
                        Console.WriteLine("The common parent is: " + firstParent);
                        Console.WriteLine("Count: " + (countOne+countTwo));
                        return;
                    }
                    else
                    {
                        secondParent = findParent(secondParent, orbits);
                    }
                    countTwo++;
                }

                firstParent = findParent(firstParent, orbits);
                secondParent = findParent(secondvalue, orbits);
                countOne++;
            }
        }

        static string findParent(string one, Dictionary<string, List<string>> orbits)
        {
            var parent = "";
            foreach (string key in orbits.Keys)
            {
                if (orbits[key].Contains(one))
                {
                    parent = key;
                }
            }
            //Console.WriteLine("Parent for " + one + ": " + parent);
            return parent;
        }

    }
    
}