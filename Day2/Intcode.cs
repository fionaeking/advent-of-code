using System;
using System.Collections.Generic;

// Intcode computer 

namespace Day2
{
    class Intcode
    {
        private List<int> puzzleInput;

        public Intcode(List<int> inputList) {
            puzzleInput = inputList;
        }
        public void Run() {
            int instructionPointer = 0;
            var instructionLength = checkInstruction(instructionPointer);
            while (instructionLength != 0)
            {
                var instructionValues = getInputValues(instructionPointer, instructionLength);
                performInstruction(instructionValues);
                instructionPointer += instructionLength;
                instructionLength = checkInstruction(instructionPointer);
            }
            printOutput(puzzleInput);
        }

        List<int> getInputValues(int offset, int length)
        {
            var inputValues = new List<int>();
            for (int i = 0; i < length; i++)
            {
                inputValues.Add(puzzleInput[i + offset]);
            }
            return inputValues;
        }

        int checkInstruction(int instructionPointer)
        {
            //Return length of instruction
            switch (puzzleInput[instructionPointer])
            {
                case Constants.ADDITION:
                case Constants.MULTIPLICATION: return 4;
                case Constants.END_OF_PROGRAM: return 0;
                default: throw new Exception("Error - unrecognised opcode");
            }
        }
        void performInstruction(List<int> instructions)
        {
            var opcode = instructions[0];
            switch (opcode)
            {
                case Constants.ADDITION:
                    puzzleInput[instructions[3]] = getValueFromPosn(instructions[1]) + getValueFromPosn(instructions[2]);
                    break;
                case Constants.MULTIPLICATION:
                    puzzleInput[instructions[3]] = getValueFromPosn(instructions[1]) * getValueFromPosn(instructions[2]);
                    break;
                default:
                    throw new Exception("Unrecognised input");
            }
        }

        int getValueFromPosn(int input)
        {
            return puzzleInput[input];
        }

        void printOutput(List<int> puzzleOutput)
        {
            Console.WriteLine("End of program. Printing out puzzle output:");
            Console.WriteLine(string.Join(",", puzzleOutput.ToArray()));
        }

    }
}
