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
                updateVelocities(io, europa);
                updateVelocities(io, ganymede);
                updateVelocities(io, callisto);
                updateVelocities(europa, ganymede);
                updateVelocities(europa, callisto);
                updateVelocities(ganymede, callisto);

                foreach (var moon in listOfMoons)
                {
                    moon.updatePosition();
                }
            }
            var totalEnergy = returnTotalEnergy(listOfMoons);
            Console.WriteLine("Total energy: " + totalEnergy);
            
        }

        static void updateVelocities(Moon moonOne, Moon moonTwo)
        {
            if (moonOne.Px < moonTwo.Px)
            {
                moonOne.updateVx(true);
                moonTwo.updateVx(false);
            }
            else if (moonOne.Px > moonTwo.Px)
            {
                moonOne.updateVx(false);
                moonTwo.updateVx(true);
            }

            if (moonOne.Py < moonTwo.Py)
            {
                moonOne.updateVy(true);
                moonTwo.updateVy(false);
            }
            else if (moonOne.Py > moonTwo.Py)
            {
                moonOne.updateVy(false);
                moonTwo.updateVy(true);
            }

            if (moonOne.Pz < moonTwo.Pz)
            {
                moonOne.updateVz(true);
                moonTwo.updateVz(false);
            }
            else if (moonOne.Pz > moonTwo.Pz)
            {
                moonOne.updateVz(false);
                moonTwo.updateVz(true);
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
