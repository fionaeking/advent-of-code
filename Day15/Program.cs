using System.Net;
using System.Xml.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleInput = puzzleInputToList (Constants.INPUT_FILENAME);
            for (int j=15; j<16; j++)
            {
                //IEnumerable<IEnumerable<int>> result =
                //getCombinations(Enumerable.Range(1, 4), j);

                var result = getCombinations(Enumerable.Range(1, 4).ToList(), j).ToList();
                //foreach (var r in result) Console.WriteLine(String.Join("", r));
                //Console.WriteLine("DONEDONEODONEONONODNONDONDODD");
                result.RemoveAll(containsRedundantInstruction);
                //foreach (var r in result) Console.WriteLine(String.Join("", r));

                Console.WriteLine("Starting for loop");

                foreach (var phaseSequence in result)
                {       
                    int count = 0;
                    Intcode i = new Intcode(puzzleInput, phaseSequence);
                    while (!i.hasFinished)
                    {
                        var xPosn = i.Run ();
                        count++;
                        if (xPosn==2)
                        {
                            Console.WriteLine("min value is " + count);
                            break;
                        }
                        else if (xPosn==0)
                        {
                            break;
                        }
                    }
                }
            }

            
        }

        // Search predicate returns true if string contains redundant instruction
        private static bool containsRedundantInstruction(List<int> testList) //(String joinedString)
        {
            var permutations = getPermutations(Enumerable.Range(1, 4).ToList(), 4);
            var joinedString = String.Join("", testList);
            return joinedString.Contains("12") 
                    | joinedString.Contains("21")
                    | joinedString.Contains("34") 
                    | joinedString.Contains("43")
                    | joinedString.Contains("132")
                    | joinedString.Contains("142")
                    | joinedString.Contains("231")
                    | joinedString.Contains("241")
                    | joinedString.Contains("314")
                    | joinedString.Contains("324")
                    | joinedString.Contains("413")
                    | joinedString.Contains("423")
                    | joinedString.Contains(permutations.ToString());
        }

        static List<Int64> puzzleInputToList (string inputFilePath) {
            var str = File.ReadLines (inputFilePath).First ();
            var listOfInts = str.Split (',').Select (Int64.Parse).ToList ();
            return listOfInts;
        }

        static IEnumerable<List<int>> getCombinations(List<int> list, int length)
        {
            // improve by removing if 1 followed by 2 or vice versa, or 3 followed by 4 or vice versa

            // repeating - e.g. otherwise identical sequences with 12 and 21 
            if (length == 1) return list.Select(t => new List<int> { t });
            return getCombinations(list, length - 1)
                .SelectMany(x => list, //.Where(o => (o.CompareTo(x.Last()) != 1 | o == 3 | o == 1)),
                    (t1, t2) => t1.Concat(new List<int> { t2 })).Select(x => x.ToList());
        }

        static IEnumerable<List<int>> getPermutations(List<int> list, int length)
        {
            // improve by removing if 1 followed by 2 or vice versa, or 3 followed by 4 or vice versa

            // repeating - e.g. otherwise identical sequences with 12 and 21 
            if (length == 1) return list.Select(t => new List<int> { t });
            return getCombinations(list, length - 1)
                .SelectMany(x => list.Where(o => !x.Contains(o)),
                    (t1, t2) => t1.Concat(new List<int> { t2 })).Select(x => x.ToList());
        }
    }
}