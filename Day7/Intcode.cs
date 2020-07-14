using System;
using System.Collections.Generic;
using System.Linq;

// Intcode computer

class Intcode
{
    private int instructionPointer;
    public List<int> puzzleInput;
    private string name;

    private bool phaseIsSet;

    private int phaseSetting;
    public Intcode(string ampName, int PhaseSetting)
    {
        instructionPointer = 0;
        //puzzleInput = inputList;
        hasFinished = false;
        name = ampName;
        phaseSetting = PhaseSetting;
        phaseIsSet = false;
    }

    public bool hasFinished
    {
        get;
        private set;
    }

    private int outputValue;

    public int Run(int newInputValue)
    {
        var opcode = getOpcode();
        var instructionLength = checkInstruction(opcode);
        while (opcode != 99)
        {
            //Get parameter modes for each value in instruction
            var instructionValues = getInputValues(puzzleInput, instructionPointer, instructionLength);
            // Get instruction pointer for next loop
            incrementInstructionPointer(instructionLength);
            performInstruction(opcode, instructionValues, newInputValue);
            if (opcode == 4)
            {
                return outputValue;
            }
            opcode = getOpcode();
            instructionLength = checkInstruction(opcode);
        }
        hasFinished = true;
        return outputValue;
    }

    List<Tuple<int, int>> getInputValues(List<int> inputNumList, int offset, int length)
    {
        var inputValues = new List<Tuple<int, int>>();

        //Check instruction - remove opcode (last 2 digits)
        int instruction = inputNumList[offset];
        int currDigits = instruction / 100;
        for (int i = 1; i < length; i++)
        {
            int mode = currDigits % 10;
            inputValues.Add(new Tuple<int, int>(inputNumList[i + offset], mode));

            currDigits /= 10;
        }
        return inputValues;
    }

    int getOpcode()
    {
        int instruction = puzzleInput[instructionPointer];
        // Only select last 2 digits for opcode
        var opcode = instruction % 100;
        return opcode;
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
    void performInstruction(int opcode, List<Tuple<int, int>> instructionValues, int newInputValue)
    {
        int firstInt, secondInt;
        //var outputValue = 0;
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
                if (!phaseIsSet)
                {
                    puzzleInput[instructionValues[0].Item1] = phaseSetting;
                    phaseIsSet = true;
                }
                else
                {
                    puzzleInput[instructionValues[0].Item1] = newInputValue;
                }
                //Console.WriteLine("Enter an input value");
                //puzzleInput[instructionValues[0].Item1] = Convert.ToInt32(Console.ReadLine());
                break;
            case Constants.OUTPUT:
                outputValue = getValueFromMode(puzzleInput, instructionValues[0]);
                break;
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
                puzzleInput[instructionValues[2].Item1] = (firstInt < secondInt) ? 1 : 0;
                break;
            case Constants.SET_ON_EQUAL:
                firstInt = getValueFromMode(puzzleInput, instructionValues[0]);
                secondInt = getValueFromMode(puzzleInput, instructionValues[1]);
                puzzleInput[instructionValues[2].Item1] = (firstInt == secondInt) ? 1 : 0;
                break;
            default:
                throw new Exception("Unrecognised input");
        }
    }

    int getValueFromMode(List<int> inputNums, Tuple<int, int> input)
    {
        return (input.Item2 == 0) ? inputNums[input.Item1] : input.Item1;
    }

    void incrementInstructionPointer(int instructionLength)
    {
        instructionPointer += instructionLength;
    }


}