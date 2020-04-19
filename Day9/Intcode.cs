using System;
using System.Collections.Generic;

// Intcode computer 

class Intcode
{
    private int instructionPointer;
    public List<long> puzzleInput;
    private long relativeBase;
    public Intcode(List<long> inputList)
    {
        instructionPointer = 0;
        puzzleInput = inputList;
        hasFinished = false;
        relativeBase = 0;
    }

    public bool hasFinished
    {
        get;
        private set;
    }

    private long outputValue;

    public long Run()
    {
        var opcode = getOpcode();
        var instructionLength = checkInstruction(opcode);
        while (opcode != 99)
        {
            //Get parameter modes for each value in instruction
            var instructionValues = getInputValues(instructionPointer, instructionLength);
            // Get instruction pointer for next loop
            incrementInstructionPointer(instructionLength);
            performInstruction(opcode, instructionValues);
            opcode = getOpcode();
            instructionLength = checkInstruction(opcode);
        }
        hasFinished = true;
        //Console.WriteLine (String.Join (",", puzzleInput));
        return outputValue;
    }

    List<Tuple<long, long>> getInputValues(int offset, int length)
    {
        var inputValues = new List<Tuple<long, long>>();
        //Check instruction - remove opcode (last 2 digits)
        long instruction = puzzleInput[offset];
        long currDigits = instruction / 100;
        for (int i = 1; i < length; i++)
        {
            long mode = currDigits % 10;
            inputValues.Add(new Tuple<long, long>(puzzleInput[i + offset], mode));

            currDigits /= 10;
        }
        return inputValues;
    }

    int getOpcode()
    {
        long instruction = puzzleInput[instructionPointer];
        // Only select last 2 digits for opcode
        var opcode = Convert.ToInt32(instruction % 100);
        return opcode;
    }

    int checkInstruction(int opcode)
    {
        // Return length of instruction
        switch (opcode)
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
            case 9:
                return 2;
            case 99:
                return 0;
            default:
                throw new Exception("Error - unrecognised opcode");
        }
    }

    void performInstruction(int opcode, List<Tuple<long, long>> instructionInputs)
    {
        long firstInput, secondInput;
        switch (opcode)
        {
            case 1: // Addition
                firstInput = getValueFromMode(instructionInputs[0]);
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], firstInput + secondInput);
                break;
            case 2: // Multiplication
                firstInput = getValueFromMode(instructionInputs[0]);
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], firstInput * secondInput);
                break;
            case 3:
                Console.WriteLine("Enter an input value");
                updateMemoryLocation(instructionInputs[0], Convert.ToInt64(Console.ReadLine()));
                break;
            case 4:
                outputValue = getValueFromMode(instructionInputs[0]);
                Console.WriteLine(outputValue);
                break;
            case 5:
                //jump-if-true
                firstInput = getValueFromMode(instructionInputs[0]);
                if (firstInput != 0)
                {
                    instructionPointer = Convert.ToInt32(getValueFromMode(instructionInputs[1]));
                }
                break;
            case 6:
                //jump-if-false: 
                firstInput = getValueFromMode(instructionInputs[0]);
                if (firstInput == 0)
                {
                    instructionPointer = Convert.ToInt32(getValueFromMode(instructionInputs[1]));
                }
                break;
            case 7:
                firstInput = getValueFromMode(instructionInputs[0]);
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], (firstInput < secondInput) ? 1 : 0);
                break;
            case 8:
                firstInput = getValueFromMode(instructionInputs[0]);
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], (firstInput == secondInput) ? 1 : 0);
                break;
            case 9:
                firstInput = getValueFromMode(instructionInputs[0]);
                relativeBase += firstInput;
                break;
            default:
                throw new Exception("Unrecognised input");
        }
    }

    long getValueFromMode(Tuple<long, long> input)
    {
        if (input.Item2 == 0)
        {
            increaseComputerMemory(input.Item1);
            return puzzleInput[Convert.ToInt32(input.Item1)];
        }
        else if (input.Item2 == 2)
        {
            increaseComputerMemory(input.Item1 + relativeBase);
            var test = puzzleInput[Convert.ToInt32(input.Item1 + relativeBase)];
            return test;
        }
        else return input.Item1;
    }

    int getIndexFromMode(Tuple<long, long> input)
    {
        return Convert.ToInt32((input.Item2 == 2) ? (input.Item1 + relativeBase) : input.Item1);
    }

    void updateMemoryLocation(Tuple<long, long> memoryLocation, long valueToWrite)
    {
        int index = getIndexFromMode(memoryLocation);
        increaseComputerMemory(index);
        puzzleInput[index] = valueToWrite;
    }

    void incrementInstructionPointer(int instructionLength)
    {
        instructionPointer += instructionLength;
    }

    void increaseComputerMemory(long index)
    {
        while (puzzleInput.Count < (index + 1))
        {
            puzzleInput.Add(0); // pad with 0s
        }
    }

}