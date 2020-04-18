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
            //var puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            int max = 0;
            IEnumerable<int> phaseSequenceMax = new int[] { };
            int outputValue;
            //var phaseSequence = new List<int>(){0,1,2,3,4};
            IEnumerable<IEnumerable<int>> result =
                getCombinations(Enumerable.Range(5, 5), 5);

            foreach (var phaseSequence in result)
            {
                outputValue = runFeedbackLoop(phaseSequence);
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

        static int runFeedbackLoop(IEnumerable<int> phaseSequence)
        {
            List<int> phaseList = phaseSequence.ToList();
            int nextOutA;
            int nextOutB;
            int nextOutC;
            int nextOutD;
            int nextOutE;
            Intcode ampA = new Intcode("A", phaseList[0]);
            Intcode ampB = new Intcode("B", phaseList[1]);
            Intcode ampC = new Intcode("C", phaseList[2]);
            Intcode ampD = new Intcode("D", phaseList[3]);
            Intcode ampE = new Intcode("E", phaseList[4]);
            ampA.puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            ampB.puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            ampC.puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            ampD.puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            ampE.puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            nextOutE = 0;
            while (!ampE.hasFinished)
            {
                nextOutA = ampA.Run(nextOutE);
                nextOutB = ampB.Run(nextOutA);
                nextOutC = ampC.Run(nextOutB);
                nextOutD = ampD.Run(nextOutC);
                nextOutE = ampE.Run(nextOutD);
                //Console.WriteLine(nextOutE);
            }
            //Console.WriteLine(nextOutE);
            return nextOutE;
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
