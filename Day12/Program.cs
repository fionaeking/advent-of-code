using System;
using System.Collections.Generic;
using System.IO;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> inputList = puzzleInputToList(Constants.INPUT_FILENAME);
            Moon io = new Moon(inputList[0]);
            Moon europa = new Moon(inputList[1]);
            Moon ganymede = new Moon(inputList[2]);
            Moon callisto = new Moon(inputList[3]);
            List<Moon> listOfMoons = new List<Moon>(){io, europa, ganymede, callisto};

            for (int timestep=0; timestep < Constants.TIMESTEP; timestep++)
            {
                //Compare pairs - 6 combinations
                callUpdateVelocities(listOfMoons);
                foreach (var moon in listOfMoons)
                {
                    moon.updatePosition();
                }
            }
            var totalEnergy = returnTotalEnergy(listOfMoons);
            Console.WriteLine("Total energy: " + totalEnergy);
            
        }

        static void callUpdateVelocities(List<Moon> moonList)
        {
            // Todo - this could definitely be more concise
            updateVelocities(moonList[0], moonList[1]);
            updateVelocities(moonList[0], moonList[2]);
            updateVelocities(moonList[0], moonList[3]);
            updateVelocities(moonList[1], moonList[2]);
            updateVelocities(moonList[1], moonList[3]);
            updateVelocities(moonList[2], moonList[3]);
        }

        static void updateVelocities(Moon moonOne, Moon moonTwo)
        {
            var posnCount = 0;
            foreach (var posn in new List<Tuple<int, int>>(){new Tuple<int, int>(moonOne.Px, moonTwo.Px),
                                                             new Tuple<int, int>(moonOne.Py, moonTwo.Py),
                                                             new Tuple<int, int>(moonOne.Pz, moonTwo.Pz)})
            {
                if (posn.Item1 < posn.Item2)
                {
                    moonOne.updateVelocity(posnCount, 1);
                    moonTwo.updateVelocity(posnCount, -1);
                }
                else if (posn.Item1 > posn.Item2)
                {
                    moonOne.updateVelocity(posnCount, -1);
                    moonTwo.updateVelocity(posnCount, 1);
                }
                posnCount++;
            }
        }

        static int returnTotalEnergy(List<Moon> listOfMoons)
        {
            return listOfMoons[0].calcTotalEnergy() + listOfMoons[1].calcTotalEnergy() 
                    + listOfMoons[2].calcTotalEnergy() + listOfMoons[3].calcTotalEnergy();
        }

        static List<List<int>> puzzleInputToList (string inputFilePath) {
            var inputFile = File.ReadLines(inputFilePath);
            List<List<int>> allMoonPosns = new List<List<int>>();
            foreach (var str in inputFile)
            {
                var substr = str.Remove(str.Length-1).Split(',');
                List<int> oneMoonPosn = new List<int>();
                foreach (var each in substr)
                {
                    oneMoonPosn.Add(Convert.ToInt32(each.Split('=')[1]));
                }
                allMoonPosns.Add(oneMoonPosn);
            }
            return allMoonPosns;
        }
    }
}
