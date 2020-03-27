using System;
using System.Collections.Generic;  
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            int validInput, inputPosnOne, inputPosnTwo, inputPosnThree, inputValueOne, inputValueTwo;
            //int count = 0;
            //int instructionPointer = 0;
            //int instructionLength = 4;
            var noun = 0;
            var verb = 0;

            for (noun=0; noun<100; noun++)
            {
                for (verb=0; verb<100; verb++)
                {
                    var inputNumList = new List<int>(){1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,9,19,1,19,5,23,2,6,23,27,1,6,27,31,2,31,9,35,1,35,6,39,1,10,39,43,2,9,43,47,1,5,47,51,2,51,6,55,1,5,55,59,2,13,59,63,1,63,5,67,2,67,13,71,1,71,9,75,1,75,6,79,2,79,6,83,1,83,5,87,2,87,9,91,2,9,91,95,1,5,95,99,2,99,13,103,1,103,5,107,1,2,107,111,1,111,5,0,99,2,14,0,0};
                    int count = 0;
                    int instructionPointer = 0;
                    int instructionLength = 4;
                    inputNumList[1] = noun;
                    inputNumList[2] = verb;
                    var check = checkIfEndOfProgram(inputNumList[0]);
                    while (!check)
                    {
                        (validInput, inputPosnOne, inputPosnTwo, inputPosnThree) = getFirstFourValues(inputNumList, instructionPointer);

                        inputValueOne = getValueFromPosn(inputNumList, inputPosnOne);
                        inputValueTwo = getValueFromPosn(inputNumList, inputPosnTwo);

                        int? outputValue;
                        outputValue = performInstruction(validInput, inputValueOne, inputValueTwo);

                        //Store outputValue in position 4
                        if (outputValue != null)
                        {
                            inputNumList[inputPosnThree] = Convert.ToInt32(outputValue);
                            //string joined = string.Join(",", inputNumList.ToArray());
                            //Console.WriteLine(joined);
                            //Console.WriteLine(inputNumList.Count);
                        }
                        count++;
                        instructionPointer = count*instructionLength;
                        check = checkIfEndOfProgram(inputNumList[0+instructionPointer]);
                    }

                    Console.WriteLine("End of program");
                    Console.WriteLine(string.Join(",", inputNumList.ToArray()));
                    //return;
                    if (inputNumList[0] == 19690720)
                    {
                        Console.WriteLine("Noun");
                        Console.WriteLine(noun);
                        Console.WriteLine("Verb");
                        Console.WriteLine(verb);
                        return;
                    }
                }
            }

            /*var check = checkIfEndOfProgram(inputNumList[0]);
            while (!check)
            {
                (validInput, inputPosnOne, inputPosnTwo, inputPosnThree) = getFirstFourValues(inputNumList, instructionPointer);
                Console.WriteLine("Validinput");

                inputValueOne = getValueFromPosn(inputNumList, inputPosnOne);
                inputValueTwo = getValueFromPosn(inputNumList, inputPosnTwo);

                int? outputValue;
                outputValue = performInstruction(validInput, inputValueOne, inputValueTwo);

                //Store outputValue in position 4
                if (outputValue != null)
                {
                    inputNumList[inputPosnThree] = Convert.ToInt32(outputValue);
                    string joined = string.Join(",", inputNumList.ToArray());
                    Console.WriteLine(joined);
                    Console.WriteLine(inputNumList.Count);
                }
                count++;
                instructionPointer = count*instructionLength;
                check = checkIfEndOfProgram(inputNumList[0+instructionPointer]);
            }

            Console.WriteLine("End of program");
            Console.WriteLine(string.Join(",", inputNumList.ToArray()));
            return; */
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

        static (int, int, int, int) getFirstFourValues(List<int> inputNumList, int offset)
        {
            return (inputNumList[0+offset], inputNumList[1+offset], inputNumList[2+offset], inputNumList[3+offset]);
        }
        
        static bool checkIfEndOfProgram(int opcode)
        {
            if (opcode == 99)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int? performInstruction(int opcode, int inputNumOne, int inputNumTwo)
        {
            int outputValue;
            switch(opcode)
            {
                case 1:
                    //Console.WriteLine("Addition");
                    outputValue = inputNumOne + inputNumTwo;
                    return outputValue;
                case 2:
                    //Console.WriteLine("Multiplication");
                    outputValue = inputNumOne * inputNumTwo;
                    return outputValue;
                case 99:
                    Console.WriteLine("End of program");
                    return null;
                default:
                    Console.WriteLine("Unrecognised input");
                    return null;
            }
        }

        static int getValueFromPosn(List<int> inputNums, int input)
        {
            return inputNums[input];
        }



    }
}
