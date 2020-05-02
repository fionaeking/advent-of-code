using System;
using System.Collections.Generic;
using System.IO;

//Dict
//Key = FUEL
//Value = FuelAmount, items to make fuel

/*Check what is needed to make fuel
For each item in list:
    Find key for that fuel in dictionary
    See what multiple of fuel is required
    Check what is needed to make fuel
    Multiply amounts by the given multiple
        If fuel is ORE, add to oreCount and remove from list
        Else substitute old fuel for new fuel in list
*/

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var oreResourcesAvailable = Constants.MAX_RESOURCES;
            Nanofactory n = new Nanofactory();
            n.inputAsDict = puzzleInputToDict(Constants.INPUT_FILENAME);
            var oreCount = 0;
            var iterationCount = 0;
            while(oreResourcesAvailable>0)
            {
                n.inputAsDict = puzzleInputToDict(Constants.INPUT_FILENAME);
                oreCount = n.getOreCount();
                oreResourcesAvailable = oreResourcesAvailable - oreCount;
                if(oreResourcesAvailable>0) //without this statement, iterationCount value was 1 too many
                    iterationCount++;
            }
            Console.WriteLine($"{iterationCount} units of fuel produced");
        }

        static Dictionary<string, List<Tuple<string, int>>> puzzleInputToDict (string inputFilePath) 
        {
            Dictionary<string, List<Tuple<string, int>>> dictOfStrings = new Dictionary<string, List<Tuple<string, int>>>();
            foreach (var str in File.ReadLines (inputFilePath))
            {
                List<Tuple<string, int>> valList = new List<Tuple<string, int>>();
                var stringComponents = str.Split ("=>");
                var splitKey = stringComponents[1].Trim().Split(" ");
                Tuple<string, int> keyTuple = new Tuple<string, int>(splitKey[1], Convert.ToInt32(splitKey[0]));
                valList.Add(keyTuple);

                var splitVal = stringComponents[0].Trim().Split(",");
                foreach (var item in splitVal)
                {
                    var sp = item.Trim().Split(" ");
                    valList.Add(new Tuple<string, int>(sp[1], Convert.ToInt32(sp[0])));
                }
                dictOfStrings[splitKey[1]] = valList;
            }
            return dictOfStrings;
        }

    }
}
