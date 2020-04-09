using System;
using System.Collections.Generic;  
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = new List<int>(){3,225,1,225,6,6,1100,1,238,225,104,0,1102,79,14,225,1101,17,42,225,2,74,69,224,1001,224,-5733,224,4,224,1002,223,8,223,101,4,224,224,1,223,224,223,1002,191,83,224,1001,224,-2407,224,4,224,102,8,223,223,101,2,224,224,1,223,224,223,1101,18,64,225,1102,63,22,225,1101,31,91,225,1001,65,26,224,101,-44,224,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,101,78,13,224,101,-157,224,224,4,224,1002,223,8,223,1001,224,3,224,1,224,223,223,102,87,187,224,101,-4698,224,224,4,224,102,8,223,223,1001,224,4,224,1,223,224,223,1102,79,85,224,101,-6715,224,224,4,224,1002,223,8,223,1001,224,2,224,1,224,223,223,1101,43,46,224,101,-89,224,224,4,224,1002,223,8,223,101,1,224,224,1,223,224,223,1101,54,12,225,1102,29,54,225,1,17,217,224,101,-37,224,224,4,224,102,8,223,223,1001,224,3,224,1,223,224,223,1102,20,53,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,107,226,226,224,1002,223,2,223,1006,224,329,101,1,223,223,1108,677,226,224,1002,223,2,223,1006,224,344,101,1,223,223,7,677,226,224,102,2,223,223,1006,224,359,101,1,223,223,108,226,226,224,1002,223,2,223,1005,224,374,101,1,223,223,8,226,677,224,1002,223,2,223,1006,224,389,101,1,223,223,1108,226,226,224,102,2,223,223,1006,224,404,101,1,223,223,1007,677,677,224,1002,223,2,223,1006,224,419,101,1,223,223,8,677,677,224,1002,223,2,223,1005,224,434,1001,223,1,223,1008,226,226,224,102,2,223,223,1005,224,449,1001,223,1,223,1008,226,677,224,102,2,223,223,1006,224,464,101,1,223,223,1107,677,677,224,102,2,223,223,1006,224,479,101,1,223,223,107,677,677,224,1002,223,2,223,1005,224,494,1001,223,1,223,1107,226,677,224,1002,223,2,223,1005,224,509,101,1,223,223,1108,226,677,224,102,2,223,223,1006,224,524,101,1,223,223,7,226,226,224,1002,223,2,223,1005,224,539,101,1,223,223,108,677,677,224,1002,223,2,223,1005,224,554,101,1,223,223,8,677,226,224,1002,223,2,223,1005,224,569,1001,223,1,223,1008,677,677,224,102,2,223,223,1006,224,584,101,1,223,223,107,226,677,224,102,2,223,223,1005,224,599,1001,223,1,223,7,226,677,224,102,2,223,223,1005,224,614,101,1,223,223,1007,226,226,224,1002,223,2,223,1005,224,629,101,1,223,223,1107,677,226,224,1002,223,2,223,1006,224,644,101,1,223,223,108,226,677,224,102,2,223,223,1006,224,659,101,1,223,223,1007,677,226,224,102,2,223,223,1006,224,674,101,1,223,223,4,223,99,226};
            int instructionPointer = 0;
            var opcode = getOpcode(puzzleInput[0]);
            var instructionLength = checkInstruction(opcode);
            while (instructionLength!=0)
            {
                //Get parameter modes for each value in instruction
                var instructionValues = getInputValues(puzzleInput, instructionPointer, instructionLength);
                // Get instruction pointer for next loop
                instructionPointer = getNewInstructionPointer(instructionPointer, instructionLength, opcode);
                instructionPointer = performInstruction(opcode, instructionValues, puzzleInput, instructionPointer);
                opcode = getOpcode(puzzleInput[0+instructionPointer]);
                instructionLength = checkInstruction(opcode);
            }

            //Console.WriteLine("Printing out puzzle output");
            //Console.WriteLine(string.Join(",", puzzleInput.ToArray()));
            return; 
        }

        static List<Tuple<int, int>> getInputValues(List<int> inputNumList, int offset, int length)
        {
            var inputValues = new List<Tuple<int, int>>();

            //Check instruction - remove opcode (last 2 digits)
            int instruction = inputNumList[offset];
            int currDigits = instruction / 100;
            for (int i=1; i<length; i++)
            {
                //inputValues.Add(inputNumList[i+offset]);
                int mode = currDigits % 10;
                inputValues.Add(new Tuple<int, int>(inputNumList[i+offset], mode));

                currDigits /= 10;
            }
            return inputValues;
        }

        static int getOpcode(int instruction)
        {
            // Only select last 2 digits for opcode
            var opcode = instruction % 100;
            return opcode;
        }

        static int checkInstruction(int opcode)
        {
            // Return length of instruction
            switch(opcode)
            {
                case 1:
                return 4;

                case 2:
                return 4;

                case 3:
                return 2;

                case 4:
                return 2;

                case 5:
                return 3;

                case 6:
                return 3;

                case 7:
                return 4;

                case 8:
                return 4;

                case 99:
                Console.WriteLine("End of program");
                return 0;

                default:
                Console.WriteLine("Error - terminating program");
                return 0;
            }
        }
        static int performInstruction(int opcode, List<Tuple<int, int>> instructionValues, List<int> puzzleInput, int instructionPointer)  //List<int>
        {
            int firstInt, secondInt, valueToWrite;
            switch(opcode)
            {
                case 1:  // Addition
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    // Note to self - using list mutability
                    puzzleInput[instructionValues[2].Item1] = firstInt + secondInt;
                    return instructionPointer;
                case 2:  // Multiplication
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    puzzleInput[instructionValues[2].Item1] = firstInt * secondInt;
                    return instructionPointer;
                case 3:
                    Console.WriteLine("Enter an input value");
                    puzzleInput[instructionValues[0].Item1] = Convert.ToInt32(Console.ReadLine());
                    return instructionPointer;
                case 4:
                    var value = getValueFromMode(puzzleInput, instructionValues[0]);
                    Console.WriteLine($"The value is {value}");
                    return instructionPointer;
                case 5:
                    //jump-if-true
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    if (firstInt != 0)
                    {
                        instructionPointer = getValueFromMode(puzzleInput, instructionValues[1]);
                    }
                    return instructionPointer;
                case 6:
                    //jump-if-false: 
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    if (firstInt == 0)
                    {
                        instructionPointer = getValueFromMode(puzzleInput, instructionValues[1]);
                    }
                    return instructionPointer;
                case 7:
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    valueToWrite = Convert.ToInt32(firstInt < secondInt);
                    puzzleInput[instructionValues[2].Item1] = valueToWrite;
                    return instructionPointer;
                case 8:
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    valueToWrite = Convert.ToInt32(firstInt == secondInt);
                    puzzleInput[instructionValues[2].Item1] = valueToWrite;
                    return instructionPointer;
                default:
                    return instructionPointer;
                    //throw new Exception("Unrecognised input");
            }
        }

        static int getValueFromMode(List<int> inputNums, Tuple<int, int> input)
        {
            if (input.Item2 == 0)
            {
                return inputNums[input.Item1];
            }
            else
            {
                return input.Item1;
            }
        }

        static int getNewInstructionPointer(int instructionPointer, int instructionLength, int opcode)
        {
            return instructionPointer += instructionLength;
        }

    }
}
