using System;
using System.Collections.Generic;

// Intcode computer 

class Intcode
{
    private int instructionPointer;
    public List<long> puzzleInput;
    public long outputValue;
    private long relativeBase;
    public bool hasFinished;
    public long ballPosn;
    public long paddlePosn;
    private List<int> newList;
    public Intcode(List<long> inputList, List<int> inputSeq)
    {
        instructionPointer = 0;
        puzzleInput = new List<long>(inputList);
        relativeBase = 0;
        hasFinished = false;
        newList = new List<int>(inputSeq);
    }

    public long Run()
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
            if (opcode == 4)
            {
                return outputValue;
            }
            opcode = getOpcode();
        }
        hasFinished = true;
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
            case 2:
            case 7:
            case 8:
                return 4;
            case 3:
            case 4:
            case 9:
                return 2;
            case 5:
            case 6:
                return 3;
            case 99:
                return 0;
            default:
                throw new Exception("Error - unrecognised opcode");
        }
    }

    void performInstruction(int opcode, List<Tuple<long, long>> instructionInputs)
    {
        long secondInput;
        long firstInput = getValueFromMode(instructionInputs[0]);
        switch (opcode)
        {
            case 1: // Addition
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], firstInput + secondInput);
                break;
            case 2: // Multiplication
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], firstInput * secondInput);
                break;
            case 3:
                var valueRead = 0;
                if (newList.Count!=0)
                {
                    valueRead = newList[0];
                    newList.RemoveAt(0);
                }
                else
                {
                    hasFinished = true;
                }
                updateMemoryLocation(instructionInputs[0], Convert.ToInt64(valueRead));
                break;
            case 4:
                outputValue = firstInput;
                break;
            case 5: //jump-if-true
                if (firstInput != 0)
                    instructionPointer = Convert.ToInt32(getValueFromMode(instructionInputs[1]));
                break;
            case 6: //jump-if-false: 
                if (firstInput == 0)
                    instructionPointer = Convert.ToInt32(getValueFromMode(instructionInputs[1]));
                break;
            case 7:
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], (firstInput < secondInput) ? 1 : 0);
                break;
            case 8:
                secondInput = getValueFromMode(instructionInputs[1]);
                updateMemoryLocation(instructionInputs[2], (firstInput == secondInput) ? 1 : 0);
                break;
            case 9:
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