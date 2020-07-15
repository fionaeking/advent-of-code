using System;
using System.Collections.Generic;
using System.Linq;

// Program to check how many different passwords within a given input range meet the set criteria

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            (int inputNumFirst, int inputNumLast) = getFirstAndLastNumbers(Constants.inputString);
            try
            {
                checkRangeIsValid(inputNumFirst, inputNumLast);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid range - terminating program");
                return;
            }

            var count = getMatchingPasswordCount(inputNumFirst, inputNumLast);
            Console.WriteLine($"{count} passwords matched this criteria");
        }

        static (int, int) getFirstAndLastNumbers(string inputAsString)
        {
            var inputNum = (inputAsString.Split('-'));
            Int32.TryParse(inputNum[0], out int inputNumFirst);
            Int32.TryParse(inputNum[1], out int inputNumLast);
            return (inputNumFirst, inputNumLast);
        }

        static void checkRangeIsValid(int inputLow, int inputHigh)
        {
            var lowCheck = checkSixDigitNumber(inputLow);
            var highCheck = checkSixDigitNumber(inputHigh);
            if ((lowCheck && highCheck) == false)
            {
                throw new Exception("Invalid range");
            }
        }

        static bool checkSixDigitNumber(int inputNum)
        {
            return 100000 <= inputNum && inputNum <= 999999;
        }

        static List<int> convertToListOfDigits(int inputNum)
        {
            List<int> listOfInts = new List<int>();
            while (inputNum > 0)
            {
                listOfInts.Add(inputNum % 10);
                inputNum = inputNum / 10;
            }
            listOfInts.Reverse();
            return listOfInts;
        }

        static bool checkAdjDigitsSame(List<int> inputNum)
        {
            for (int digit = 0; digit < 5; digit++)
            {
                if (inputNum[digit] == inputNum[digit + 1])
                {
                    switch (digit)
                    {
                        case 0:
                            if (inputNum[digit] != inputNum[digit + 2])
                            {
                                return true;
                            }
                            break;
                        case 4:
                            if (inputNum[digit] != inputNum[digit - 1])
                            {
                                return true;
                            }
                            break;
                        default:
                            if ((inputNum[digit] != inputNum[digit + 2]) && (inputNum[digit] != inputNum[digit - 1]))
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
            for (int i = 0; i < 5; i++)
            {
                if (inputNum[i + 1] < inputNum[i])
                {
                    return false;
                }
            }
            return true;
        }

        static int getMatchingPasswordCount(int inputNumFirst, int inputNumLast)
        {
            int currNum;
            int count = 0;

            for (currNum = inputNumFirst; currNum <= inputNumLast; currNum++)
            {
                bool[] checkVar = new bool[2];
                List<int> listOfInts = convertToListOfDigits(currNum);
                checkVar[0] = checkAdjDigitsSame(listOfInts);
                checkVar[1] = checkDigitsNeverDecrease(listOfInts);
                if (!checkVar.Contains(false))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
