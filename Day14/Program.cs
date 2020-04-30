using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



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
            var inputAsDict = puzzleInputToDict(Constants.INPUT_FILENAME);
            var reqdItems = new List<string>();
            var reqdAmounts = new List<string>();
            var oreCount = 0;
            var startingVal = inputAsDict["FUEL"];

            //foreach (var item in startingVal) Console.WriteLine(String.Join("", item));

            while(startingVal.Count>1)
            {
                //Console.WriteLine(startingVal.Count);
                startingVal = new List<Tuple<string, int>>(inputAsDict["FUEL"]);
                foreach (var item in startingVal) Console.WriteLine(String.Join("", item));
                for (int i=1; i<startingVal.Count; i++)
                {
                    if (startingVal[i].Item1=="ORE")
                    {
                        oreCount += startingVal[i].Item2;
                        Console.WriteLine("Ore count: " + oreCount);
                        inputAsDict["FUEL"].Remove(startingVal[i]);
                        //break;
                    }
                    else
                    {
                        var inputFuel = inputAsDict[startingVal[i].Item1];
                        var inputFuelAmount = inputFuel[0].Item2;
                        var outputFuelAmount = startingVal[i].Item2;
                        var multiplier = 1;
                        while(inputFuelAmount<outputFuelAmount)
                        {
                            multiplier += 1;
                            inputFuelAmount += inputFuelAmount;
                        }
                        for (int j=1; j<inputFuel.Count; j++)
                        {
                            var newAmount = inputFuel[j].Item2 * multiplier;
                            inputAsDict["FUEL"].Add(new Tuple<string, int>(inputFuel[j].Item1, newAmount));
                        }
                        inputAsDict["FUEL"].Remove(startingVal[i]);
                    }
                    //}
                }
            }
            Console.WriteLine(oreCount);

            
        }

         static Dictionary<string, List<Tuple<string, int>>> puzzleInputToDict (string inputFilePath) {
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
