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
            var oreResourcesAvailable = 1000000000000;
            //var iterationCount=0;
            Nanofactory n = new Nanofactory();
            n.inputAsDict = puzzleInputToDict(Constants.INPUT_FILENAME);
            var oreCount = n.getOreCount();
            var storeExcess = new Dictionary<string, long>(n.excess);
            var iterationCount = Convert.ToInt64(0.8 * oreResourcesAvailable/oreCount);
            //var iterationCount = 5200000;
            foreach (var kv in storeExcess)
            {
                n.excess[kv.Key] = n.excess[kv.Key] * iterationCount;
            }
            long subtract = Convert.ToInt64(iterationCount)*Convert.ToInt64(oreCount);
            oreResourcesAvailable = oreResourcesAvailable - subtract;

            /*bool isNegative = false;
            //var storeExcessFinal = new Dictionary<string, long>(n.excess);
            while(!isNegative)
            {
                var storeExcessFinal = new Dictionary<string, long>(n.excess);
                //Console.WriteLine("In while loop");
                foreach (var kv in storeExcessFinal)
                {
                    isNegative = kv.Value-storeExcess[kv.Key]<0;
                }
                foreach (var kv in storeExcessFinal)
                {
                    n.excess[kv.Key] -= storeExcess[kv.Key];
                }
                iterationCount++;
            }
            Console.WriteLine("out while loop");  */
            
            while(oreResourcesAvailable>0)
            {
                n.inputAsDict = puzzleInputToDict(Constants.INPUT_FILENAME);
                oreCount = n.getOreCount();
                //Console.WriteLine("ore count: " + oreCount);
                oreResourcesAvailable -= oreCount;
                if (oreResourcesAvailable>0)
                    iterationCount++;
            }
            
            /*while(true)
            {
                var storeExcessFinal = new Dictionary<string, long>(n.excess);
                Console.WriteLine("In while loop");
                foreach (var kv in storeExcessFinal)
                {
                    if(kv.Value-storeExcess[kv.Key]<0)
                    {
                        Console.WriteLine($"{iterationCount} units of fuel produced");
                        return;
                    }
                    else
                    {
                        n.excess[kv.Key] -= storeExcess[kv.Key];
                    }
                }
                iterationCount++;
            }*/
        
            Console.WriteLine($"{iterationCount} units of fuel produced");
        }

        static Dictionary<string, List<Tuple<string, int>>> puzzleInputToDict (string inputFilePath) 
        {
            Dictionary<string, List<Tuple<string, int>>> dictOfStrings = new Dictionary<string, List<Tuple<string, int>>>();
            
            var listOfStrings = File.ReadLines (inputFilePath);
            foreach (var str in listOfStrings)
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
