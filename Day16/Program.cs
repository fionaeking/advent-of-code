using System;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var prevOutputString = puzzleInputToList(Constants.INPUT_FILENAME);
            var finalPhase = 100;
            var finalOutput = getOutputAfterPhase(prevOutputString, finalPhase);
            Console.WriteLine($"First 8 digits after phase {finalPhase}: {String.Join("", finalOutput.Take(8))}");
        }

        static int[] puzzleInputToList (string inputFilePath) 
        {
            var str = File.ReadLines (inputFilePath).First();
            var listOfInts = str.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
            return listOfInts;
        }

        static int[] getOutputAfterPhase(int[] prevOutputString, int finalPhase)
        {
            var newInputString = new int[8];
            var basePattern = new int[4]{0, 1, 0, -1};
            for (int phase=1; phase<=finalPhase; phase++)
            {
                newInputString = prevOutputString;
                for (int i=0; i<newInputString.Length; i++)
                {
                    var basePatternRepeated = basePattern.SelectMany(t =>
                        Enumerable.Repeat(t, i+1)).ToList();
                    var summedTotal = 0;
                    var basePatternIndex = 1;
                    for (int j=0; j<newInputString.Length; j++)
                    {
                        summedTotal += newInputString[j]*basePatternRepeated[basePatternIndex];
                        basePatternIndex = (basePatternIndex==basePatternRepeated.Count-1) ? 0 : basePatternIndex+1;
                    }
                    prevOutputString[i] = Math.Abs(summedTotal%10); //only take ones digit
                }
            }
            return prevOutputString;
        }
    }
}
