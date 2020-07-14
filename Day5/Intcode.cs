using System;
using System.Collections.Generic;
using System.Linq;

// Intcode computer 

class Intcode
{
    private int instructionPointer;
    private List<int> puzzleInput;
    public Intcode(List<int> inputList)
    {
        instructionPointer = 0;
        puzzleInput = inputList;
    }

    public void Run()
    {
        List<int> outputDiagnosticCodes = new List<int>();
        var opcode = getOpcode(puzzleInput[0]);
        var instructionLength = checkInstruction(opcode);
        while (instructionLength != 0)
        {
            //Get parameter modes for each value in instruction
            var instructionValues = getInputValues(puzzleInput, instructionPointer, instructionLength);
            // Get instruction pointer for next loop
            getNewInstructionPointer(instructionLength);
            performInstruction(opcode, instructionValues, outputDiagnosticCodes);
            opcode = getOpcode(puzzleInput[instructionPointer]);
            instructionLength = checkInstruction(opcode);
        }
        printOutDiagnosticCode(outputDiagnosticCodes.LastOrDefault());
    }

    List<Tuple<int, int>> getInputValues(List<int> inputNumList, int offset, int length)
    {
        var inputValues = new List<Tuple<int, int>>();
        //Check instruction - remove opcode (last 2 digits)
        int instruction = inputNumList[offset];
        int currDigits = instruction / 100;
        for (int i = 1; i < length; i++)
        {
            //inputValues.Add(inputNumList[i+offset]);
            int mode = currDigits % 10;
            inputValues.Add(new Tuple<int, int>(inputNumList[i + offset], mode));
            currDigits /= 10;
        }
        return inputValues;
    }

    int getOpcode(int instruction)
    {
        // Only select last 2 digits for opcode
        return instruction % 100;
    }

    int checkInstruction(int opcode)
    {
        // Return length of instruction
        switch (opcode)
        {
            case Constants.ADDITION:
            case Constants.MULTIPLICATION: 
            case Constants.SLT: 
            case Constants.SET_ON_EQUAL: return 4;
            case Constants.INPUT:
            case Constants.OUTPUT: return 2;
            case Constants.BEQ: 
            case Constants.BNE: return 3;
            case Constants.END_OF_PROGRAM: return 0;
            default: throw new Exception("Error - unrecognised opcode");
        }
    }
    void performInstruction(int opcode, List<Tuple<int, int>> instructionValues, List<int> outputDiagnosticCodes)
    {
        int firstInt, secondInt, valueToWrite;
        var outputValue = 0;
        switch (opcode)
        {
            case Constants.ADDITION:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                // Note to self - using list mutability
                puzzleInput[instructionValues[2].Item1] = firstInt + secondInt;
                return;
            case Constants.MULTIPLICATION:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                puzzleInput[instructionValues[2].Item1] = firstInt * secondInt;
                return;
            case Constants.INPUT:
                Console.WriteLine("Enter an input value");
                puzzleInput[instructionValues[0].Item1] = Convert.ToInt32(Console.ReadLine());
                return;
            case Constants.OUTPUT:
                outputValue = getValueFromMode(puzzleInput, instructionValues[0]);
                Console.WriteLine($"The value is {outputValue}");
                outputDiagnosticCodes.Add(outputValue);
                return;
            case Constants.BEQ:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                if (firstInt != 0)
                {
                    instructionPointer = getValueFromMode(puzzleInput, instructionValues[1]);
                }
                return;
            case Constants.BNE:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                if (firstInt == 0)
                {
                    instructionPointer = getValueFromMode(puzzleInput, instructionValues[1]);
                }
                return;
            case Constants.SLT:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                valueToWrite = Convert.ToInt32(firstInt < secondInt);
                puzzleInput[instructionValues[2].Item1] = valueToWrite;
                return;
            case Constants.SET_ON_EQUAL:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                valueToWrite = Convert.ToInt32(firstInt == secondInt);
                puzzleInput[instructionValues[2].Item1] = valueToWrite;
                return;
            default:
                throw new Exception("Unrecognised input");
        }
    }

    int getValueFromMode(List<int> inputNums, Tuple<int, int> input)
    {
        return (input.Item2 == 0) ? inputNums[input.Item1] : input.Item1;
    }

    void getNewInstructionPointer(int instructionLength)
    {
        instructionPointer += instructionLength;
    }

    void printOutDiagnosticCode(int code)
    {
        Console.WriteLine("End of program");
        Console.WriteLine("The diagnostic code is {0}", code);
    }

}