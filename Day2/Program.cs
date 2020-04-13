using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Intcode computer 

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            int instructionPointer = 0;
            var instructionLength = checkInstruction(puzzleInput[0]);
            while (instructionLength != 0)
            {
                var instructionValues = getInputValues(puzzleInput, instructionPointer, instructionLength);
                performInstruction(instructionValues, puzzleInput);
                instructionPointer += instructionLength;
                instructionLength = checkInstruction(puzzleInput[0 + instructionPointer]);
            }
            printOutput(puzzleInput);
        }

        static List<int> puzzleInputToList(string inputFilePath)
        {
            var str = File.ReadLines(inputFilePath).First();
            var listOfInts = str.Split(',').Select(int.Parse).ToList();
            return listOfInts;
        }

        static List<int> getInputValues(List<int> inputNumList, int offset, int length)
        {
            var inputValues = new List<int>();
            for (int i = 0; i < length; i++)
            {
                inputValues.Add(inputNumList[i + offset]);
            }
            return inputValues;
        }

        static int checkInstruction(int opcode)
        {
            //Return length of instruction
            switch (opcode)
            {
                case 1: return 4;
                case 2: return 4;
                case 99: return 0;
                default: throw new Exception("Error - unrecognised opcode");
            }
        }
        static void performInstruction(List<int> instructionValues, List<int> puzzleInput)  //List<int>
        {
            int firstInt, secondInt;
            var opcode = instructionValues[0];
            switch (opcode)
            {
                case 1:  // Addition
                    firstInt = getValueFromPosn(puzzleInput, instructionValues[1]);
                    secondInt = getValueFromPosn(puzzleInput, instructionValues[2]);
                    // Note to self - using list mutability
                    puzzleInput[instructionValues[3]] = firstInt + secondInt;
                    break;
                case 2:  // Multiplication
                    firstInt = getValueFromPosn(puzzleInput, instructionValues[1]);
                    secondInt = getValueFromPosn(puzzleInput, instructionValues[2]);
                    puzzleInput[instructionValues[3]] = firstInt * secondInt;
                    break;
                default:
                    throw new Exception("Unrecognised input");
            }
        }

        static int getValueFromPosn(List<int> inputNums, int input)
        {
            return inputNums[input];
        }

        static void printOutput(List<int> puzzleOutput)
        {
            Console.WriteLine("End of program. Printing out puzzle output:");
            Console.WriteLine(string.Join(",", puzzleOutput.ToArray()));
        }

    }
}
