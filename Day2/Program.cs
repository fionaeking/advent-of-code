using System;
using System.Collections.Generic;  
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = new List<int>(){1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,9,19,1,19,5,23,2,6,23,27,1,6,27,31,2,31,9,35,1,35,6,39,1,10,39,43,2,9,43,47,1,5,47,51,2,51,6,55,1,5,55,59,2,13,59,63,1,63,5,67,2,67,13,71,1,71,9,75,1,75,6,79,2,79,6,83,1,83,5,87,2,87,9,91,2,9,91,95,1,5,95,99,2,99,13,103,1,103,5,107,1,2,107,111,1,111,5,0,99,2,14,0,0};
            int instructionPointer = 0;
            var instructionLength = checkInstruction(puzzleInput[0]);
            while (instructionLength!=0)
            {
                var instructionValues = getInputValues(puzzleInput, instructionPointer, instructionLength);
                performInstruction(instructionValues, puzzleInput);
                instructionPointer += instructionLength;
                instructionLength = checkInstruction(puzzleInput[0+instructionPointer]);
            }
            
            Console.WriteLine("Printing out puzzle output");
            Console.WriteLine(string.Join(",", puzzleInput.ToArray()));
            return; 
        }

        static List<int> getValidInput()
        {
            Console.WriteLine("Enter a comma separated list");
            var inputString = Console.ReadLine();
            //TODO - Check if int
            List<int> ids = inputString.Split(',').Select(int.Parse).ToList();
            while (ids.Count < 4)
            {
                Console.WriteLine("Input is too short");
                Console.WriteLine("Enter a comma separated list");
                inputString = Console.ReadLine();
                ids = inputString.Split(',').Select(int.Parse).ToList();
            }
            return ids;
        }

        static List<int> getInputValues(List<int> inputNumList, int offset, int length)
        {
            var inputValues = new List<int>();
            for (int i=0; i<length; i++)
            {
                inputValues.Add(inputNumList[i+offset]);
            }
            return inputValues;
        }
        
        static int checkInstruction(int opcode)
        {
            switch(opcode)
            {
                case 1:
                return 4;

                case 2:
                return 4;
                
                case 99:
                Console.WriteLine("End of program");
                return 0;

                default:
                Console.WriteLine("Error - terminating program");
                return 0;
            }
        }
        static void performInstruction(List<int> instructionValues, List<int> puzzleInput)  //List<int>
        {
            int outputValue, firstInt, secondInt;
            switch(instructionValues[0])
            {
                case 1:  // Addition
                    firstInt = getValueFromPosn(puzzleInput, instructionValues[1]);
                    secondInt = getValueFromPosn(puzzleInput, instructionValues[2]);
                    outputValue = firstInt + secondInt;
                    // Note to self - using list mutability
                    puzzleInput[instructionValues[3]] = Convert.ToInt32(outputValue);
                    return;
                case 2:  // Multiplication
                    firstInt = getValueFromPosn(puzzleInput, instructionValues[1]);
                    secondInt = getValueFromPosn(puzzleInput, instructionValues[2]);
                    outputValue = firstInt * secondInt;
                    puzzleInput[instructionValues[3]] = Convert.ToInt32(outputValue);
                    return;
                default:
                    throw new Exception("Unrecognised input");
            }
        }

        static int getValueFromPosn(List<int> inputNums, int input)
        {
            return inputNums[input];
        }



    }
}
