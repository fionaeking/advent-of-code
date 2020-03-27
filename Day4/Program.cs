using System;
using System.Collections.Generic;  
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputAsString = "125730-579381";
            (int inputNumFirst, int inputNumLast) = getFirstAndLastNumbers(inputAsString);
            int currNum;
            int count = 0;

            for (currNum = inputNumFirst; currNum <= inputNumLast; currNum++)
            {
                bool[] checkVar = new bool[3];
                checkVar[0] = checkSixDigitNumber(currNum);
                List<int> listOfInts = convertToListOfDigits(currNum);
                checkVar[1] = checkAdjDigitsSame(listOfInts);
                checkVar[2] = checkDigitsNeverDecrease(listOfInts);
                if (!checkVar.Contains(false))
                {
                    count++;
                }
            }
            Console.WriteLine($"{count} passwords matched this criteria");
        }

        static (int, int) getFirstAndLastNumbers(string inputAsString)
        {
            var inputNum = (inputAsString.Split('-'));
            int inputNumFirst;
            int inputNumLast;
            Int32.TryParse(inputNum[0], out inputNumFirst);
            Int32.TryParse(inputNum[1], out inputNumLast);
            return (inputNumFirst, inputNumLast);
        }

        static bool checkSixDigitNumber(int inputNum)
        {
            if (inputNum < 100000 | inputNum > 999999)
            {
                Console.WriteLine("Invalid input");
                return false;
            }
            else
            {
                return true;
            }
        }

        static List<int> convertToListOfDigits(int inputNum)
        {
            List<int> listOfInts = new List<int>();
            while(inputNum > 0)
            {
                listOfInts.Add(inputNum % 10);
                inputNum = inputNum / 10;
            }
            listOfInts.Reverse();
            return listOfInts;
        }

        static bool checkAdjDigitsSame(List<int> inputNum)
        {
            for (int i=0; i<5; i++)
            {
                if (inputNum[i] == inputNum[i+1])
                {
                    switch(i)
                    {
                        case 0:
                        if (inputNum[i] != inputNum[i+2])
                        {
                            return true;
                        }
                        break;
                        case 4:
                        if (inputNum[i] != inputNum[i-1])
                        {
                            return true;
                        }
                        break;
                        default:
                        if ((inputNum[i] != inputNum[i+2]) && (inputNum[i] != inputNum[i-1]))
                        {
                            return true;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        static bool checkDigitsNeverDecrease(List<int> inputNum)
        {
            for (int i=0; i<5; i++)
            {
                if (inputNum[i+1] < inputNum[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
