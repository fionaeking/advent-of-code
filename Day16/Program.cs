using System;
using System.IO;
using System.Linq;

// Flawed Frequency Transmission

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var originalInput = puzzleInputToList(Constants.INPUT_FILENAME);
            var prevOutputString = Enumerable.Repeat(originalInput, Constants.REPEAT).SelectMany(arr => arr).ToArray();
            var finalOutput = getOutputAfterPhase(prevOutputString, Constants.FINAL_PHASE);
            Console.WriteLine(finalOutput);
            var messageOffset = getMessageOffset(originalInput, finalOutput.Length);
            Console.WriteLine(messageOffset);
            Console.WriteLine($"First 8 digits after phase {Constants.FINAL_PHASE}: {String.Join("", finalOutput.Skip(messageOffset).Take(8))}");
        }

        static int getMessageOffset(int[] inputArray, int outputArrayLength)
        {
            var messageOffset = 0;
            foreach (var digit in inputArray.Take(7))
            {
                messageOffset *= 10;
                messageOffset += digit;
            }
            return messageOffset % outputArrayLength;

        }

        static int[] puzzleInputToList (string inputFilePath) 
        {
            var inputAsString = File.ReadLines (inputFilePath).First();
            var listOfInts = inputAsString.ToCharArray().Select(c => Convert.ToInt32(c.ToString())).ToArray();
            return listOfInts;
        }

        static int[] getOutputAfterPhase(int[] prevOutputString, int finalPhase)
        {
            var newInputString = new int[8];
            var basePattern = new int[4]{0, 1, 0, -1};
            for (int phase=1; phase<=finalPhase; phase++)
            {
                Console.WriteLine("At phase " + phase);
                newInputString = prevOutputString;
                for (int i=0; i<newInputString.Length; i++)
                {
                    //Console.WriteLine("At integer " + i);
                    var basePatternRepeated = basePattern.SelectMany(t =>
                        Enumerable.Repeat(t, i+1)).ToList();
                    var summedTotal = 0;
                    var basePatternIndex = 1;
                    for (int j=0; j<newInputString.Length; j++)
                    //for (int k = 0; k<newInputString.Length/2; k++)
                    {
                        var bp = basePatternRepeated[basePatternIndex];
                        if (bp!=0)
                        {
                            summedTotal += newInputString[j]*bp;
                        }
                        basePatternIndex = (basePatternIndex==basePatternRepeated.Count-1) ? 0 : basePatternIndex+1;
                    }
                    prevOutputString[i] = Math.Abs(summedTotal%10); //only take ones digit
                }
            }
            return prevOutputString;
        }

        // Half of all basePatternIndex calculations will be 0 - can half the number of calculations

    }
}
