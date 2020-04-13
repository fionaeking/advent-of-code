using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace Day7
{
    class Program
    {  //0,1,2,3,4
        static void Main(string[] args)
        {
            int outputValue = 0;
            var puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            int max = 0;
            IEnumerable<int> phaseSequenceMax = new int[] { };

            //var phaseSequence = new List<int>(){0,1,2,3,4};
            IEnumerable<IEnumerable<int>> result =
                getCombinations(Enumerable.Range(0, 5), 5);

            foreach (var phaseSequence in result)
            {
                outputValue = 0;
                foreach (var phaseSetting in phaseSequence)
                {
                    outputValue = Intcode.Run(puzzleInput, phaseSetting, outputValue);
                }
                if (outputValue > max)
                {
                    max = outputValue;
                    phaseSequenceMax = phaseSequence;
                }
            }

            Console.WriteLine(max);
            Console.WriteLine(String.Join(',', phaseSequenceMax));
        }

        static List<int> puzzleInputToList(string inputFilePath)
        {
            var str = File.ReadLines(inputFilePath).First();
            var listOfInts = str.Split(',').Select(int.Parse).ToList();
            return listOfInts;
        }

        //TODO/Confession - Took this function off StackOverflow without understanding yet
        static IEnumerable<IEnumerable<T>> getCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return getCombinations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

    }
}
