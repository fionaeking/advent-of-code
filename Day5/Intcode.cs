using System;
using System.Collections.Generic;

class Intcode {

    public static void Run(List<int> puzzleInput)
    {
            int instructionPointer = 0;
            int outputValue = 0;
            var opcode = getOpcode(puzzleInput[0]);
            var instructionLength = checkInstruction(opcode);
            while (instructionLength!=0)
            {
                //Get parameter modes for each value in instruction
                var instructionValues = getInputValues(puzzleInput, instructionPointer, instructionLength);
                // Get instruction pointer for next loop
                instructionPointer = getNewInstructionPointer(instructionPointer, instructionLength, opcode);
                outputValue = performInstruction(opcode, instructionValues, puzzleInput, instructionPointer);
                opcode = getOpcode(puzzleInput[0+instructionPointer]);
                instructionLength = checkInstruction(opcode);
            }

            Console.WriteLine("The diagnostic code is {0}", outputValue);
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
        static int performInstruction(int opcode, List<Tuple<int, int>> instructionValues, List<int> puzzleInput, int instructionPointer)
        {
            int firstInt, secondInt, valueToWrite;
            var outputValue = 0;
            switch(opcode)
            {
                case 1:  // Addition
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    // Note to self - using list mutability
                    puzzleInput[instructionValues[2].Item1] = firstInt + secondInt;
                    return outputValue;
                case 2:  // Multiplication
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    puzzleInput[instructionValues[2].Item1] = firstInt * secondInt;
                    return outputValue;
                case 3:
                    Console.WriteLine("Enter an input value");
                    puzzleInput[instructionValues[0].Item1] = Convert.ToInt32(Console.ReadLine());
                    return outputValue;
                case 4:
                    outputValue = getValueFromMode(puzzleInput, instructionValues[0]);
                    Console.WriteLine($"The value is {outputValue}");
                    return outputValue;
                case 5:
                    //jump-if-true
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    if (firstInt != 0)
                    {
                        instructionPointer = getValueFromMode(puzzleInput, instructionValues[1]);
                    }
                    return outputValue;
                case 6:
                    //jump-if-false: 
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    if (firstInt == 0)
                    {
                        instructionPointer = getValueFromMode(puzzleInput, instructionValues[1]);
                    }
                    return outputValue;
                case 7:
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    valueToWrite = Convert.ToInt32(firstInt < secondInt);
                    puzzleInput[instructionValues[2].Item1] = valueToWrite;
                    return outputValue;
                case 8:
                    firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                    secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                    valueToWrite = Convert.ToInt32(firstInt == secondInt);
                    puzzleInput[instructionValues[2].Item1] = valueToWrite;
                    return outputValue;
                default:
                    return outputValue;
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