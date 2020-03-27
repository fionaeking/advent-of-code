using System;
using System.Collections.Generic;  
using System.IO;

namespace Day1Puzzle1
{
    class Program
    {
        static void Main(string[] args)
        {
            int outputNum;
            var logFile = File.ReadAllLines("C:/Users/fk/Documents/AdventOfCode/Day1Puzzle1/InputList.txt");
            var inputNumList = new List<string>(logFile);
            outputNum = callCalculateFuelConsumption(inputNumList);
            Console.WriteLine("Fuel consumption: " + outputNum);
        }

        static int roundDown(float varOne)
        {
            return Convert.ToInt32(varOne);
        }

        static float divideByNum(int input_num, int divisor)
        {
            return input_num/divisor;
        }

        static int subtractNum(int original_num, int num_to_subtract)
        {
            return original_num - num_to_subtract;
        }

        static int calculateFuelConsumption(int inputNum)
        {
            float varOne;
            int varTwo, varThree;
            varOne = divideByNum(inputNum, 3);
            varTwo = roundDown(varOne);
            varThree = subtractNum(varTwo, 2);
            return varThree;
        }

        static int callCalculateFuelConsumption(List<string> listOfInputs)
        {
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
