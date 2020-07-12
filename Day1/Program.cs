using System;
using System.Collections.Generic;  
using System.IO;
using System.Linq;
using static Utilities.Utilities;

//Program to calculate fuel required to launch a number of given modules
//based on their mass.

namespace Day1Puzzle1
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputNumList = returnInputAsList();
            Console.WriteLine("Fuel consumption: " + calculateTotalFuelConsumption(inputNumList));
        }

        static List<string> returnInputAsList()
        {
            return File.ReadLines(Constants.INPUT_FILENAME).ToList();
        }

        static int calculateFuelConsumption(int mass)
        {
            //Finds the fuel required for a module
            //Takes its mass, divides by three, rounds down and subtracts 2
            return subtractNum(roundDown(divideByNum(mass, 3)), 2);
        }

        static int calculateTotalFuelConsumption(List<string> listOfInputs)
        {
            //To find total fuel requirement, individually calculate the fuel 
            //needed for the mass of each module (InputList.txt), then sum together.
            //Also take into account the mass of the added fuel.
            int count = 0;
            for (int i=0; i<listOfInputs.Count; i++)
            {
                var fuel = calculateFuelConsumption(Convert.ToInt32(listOfInputs[i]));
                while (fuel > 0)
                {
                    count += fuel;
                    fuel = calculateFuelConsumption(fuel);
                }
            }
            return count;
        }

    }
}
