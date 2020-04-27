using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputAsDict = puzzleInputToDict(Constants.INPUT_FILENAME);
            var startingVal = inputAsDict[new Tuple<string, int>("FUEL", 1)];
            foreach (var input in startingVal)
            {
                var reqdItem = input.Item1;
                var reqdAmount = input.Item2;

               
            }
        }

         static Dictionary<Tuple<string, int>, List<Tuple<string, int>>> puzzleInputToDict (string inputFilePath) {
            Dictionary<Tuple<string, int>, List<Tuple<string, int>>> dictOfStrings = new Dictionary<Tuple<string, int>, List<Tuple<string, int>>>();
            
            var listOfStrings = File.ReadLines (inputFilePath);
            List<Tuple<string, int>> valList = new List<Tuple<string, int>>();
            foreach (var str in listOfStrings)
            {
                var stringComponents = str.Split ("=>");
                var splitKey = stringComponents[1].Trim().Split(" ");
                Tuple<string, int> keyTuple = new Tuple<string, int>(splitKey[1], Convert.ToInt32(splitKey[0]));
                var splitVal = stringComponents[0].Trim().Split(",");

                foreach (var item in splitVal)
                {
                    var sp = item.Trim().Split(" ");
                    valList.Add(new Tuple<string, int>(sp[1], Convert.ToInt32(sp[0])));
                }
                dictOfStrings[keyTuple] = valList;
            }
            return dictOfStrings;
        }
    }
}
