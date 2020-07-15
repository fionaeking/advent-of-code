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
        relativeBase = 0;
    }

    public void Run()
    {
        var opcode = getOpcode();
        while (opcode != 99)
        {
            var instructionLength = checkInstruction(opcode);
            //Get parameter modes for each value in instruction
            var instructionValues = getInputValues(instructionPointer, instructionLength);
            // Get instruction pointer for next loop
            incrementInstructionPointer(instructionLength);
            performInstruction(opcode, instructionValues);
            opcode = getOpcode();
        }
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
        // Return opcode (last 2 digits of instruction)
        return Convert.ToInt32(puzzleInput[instructionPointer] % 100);
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
            case Constants.OUTPUT:
            case Constants.RELATIVE_BASE: return 2;
            case Constants.BEQ: 
            case Constants.BNE: return 3;
            case Constants.END_OF_PROGRAM: return 0;
            default: throw new Exception("Error - unrecognised opcode");
        }
    }

    void performInstruction(int opcode, List<Tuple<long, long>> instructions)
    {
        long secondInput;
        long firstInput = getValueFromMode(instructions[0]);
        switch (opcode)
        {
            case Constants.ADDITION:
                secondInput = getValueFromMode(instructions[1]);
                updateMemoryLocation(instructions[2], firstInput + secondInput);
                break;
            case Constants.MULTIPLICATION:
                secondInput = getValueFromMode(instructions[1]);
                updateMemoryLocation(instructions[2], firstInput * secondInput);
                break;
            case Constants.INPUT:
                Console.WriteLine("Enter an input value");
                updateMemoryLocation(instructions[0], Convert.ToInt64(Console.ReadLine()));
                break;
            case Constants.OUTPUT:
                Console.WriteLine(firstInput); // Write value to output
                break;
            case Constants.BEQ:
                if (firstInput != 0)
                    instructionPointer = Convert.ToInt32(getValueFromMode(instructions[1]));
                break;
            case Constants.BNE:
                if (firstInput == 0)
                    instructionPointer = Convert.ToInt32(getValueFromMode(instructions[1]));
                break;
            case Constants.SLT:
                secondInput = getValueFromMode(instructions[1]);
                updateMemoryLocation(instructions[2], (firstInput < secondInput) ? 1 : 0);
                break;
            case Constants.SET_ON_EQUAL:
                secondInput = getValueFromMode(instructions[1]);
                updateMemoryLocation(instructions[2], (firstInput == secondInput) ? 1 : 0);
                break;
            case Constants.RELATIVE_BASE:
                relativeBase += firstInput;
                break;
            default:
                throw new Exception("Unrecognised input");
        }
    }

    long getValueFromMode(Tuple<long, long> input)
    {
        switch(input.Item2)
        {
            case 0:
                increaseComputerMemory(input.Item1);
                return puzzleInput[Convert.ToInt32(input.Item1)];
            case 1:
                return input.Item1;
            case 2:
                increaseComputerMemory(input.Item1 + relativeBase);
                var test = puzzleInput[Convert.ToInt32(input.Item1 + relativeBase)];
                return test;
            default:
                throw new Exception("Unrecognised mode");
        }
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
            puzzleInput.Add(0); // pad with 0s
    }

}