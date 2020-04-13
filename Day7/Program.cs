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

        //TODO/Confession - Took this function off StackOverflow
        static IEnumerable<IEnumerable<int>> getCombinations(IEnumerable<int> list, int length)
        {
            if (length == 1) return list.Select(t => new int[] { t });
            // Calls itself until length=1
            // SelectMany() can turn a two-dimensional array into a single sequence of values (i.e. will concatenate)
            // SelectMany(array => array) would cause the elements of the constituent arrays to be copied into the resultant 
            // sequence without alteration.
            return getCombinations(list, length - 1)
                .SelectMany(x => list.Where(e => !x.Contains(e)),
                    (t1, t2) => t1.Concat(new int[] { t2 }));
        }

        // where there is an element in the original list that is not contained within the current resulting list
        // x is elements of the current resulting list

    }
}
