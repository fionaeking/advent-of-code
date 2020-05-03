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
            var puzzleInput = puzzleInputToList(Constants.INPUT_FILENAME);
            var startingList = new List<List<int>>(){new List<int>(){1}, 
                                                              new List<int>(){2}, 
                                                              new List<int>(){3}, 
                                                              new List<int>(){4}};
            var count = fewestMovesToOxygen(puzzleInput, startingList);
            Console.WriteLine("Min value is " + count);
        }

        static void addElement(List<List<int>> startingList, List<int> elem, int numToAdd)
        {
            startingList.Add(new List<int>(elem).Concat(new List<int>(){numToAdd}).ToList());
        }

        static void buildUpFunction(List<List<int>> startingList)
        {
            List<List<int>> copyOfInput = new List<List<int>>(startingList);
            foreach (var elem in copyOfInput)
            {
                switch(elem.Last())
                {
                    case 1:
                    case 2:
                    addElement(startingList, elem, elem.Last());
                    addElement(startingList, elem, 3);
                    addElement(startingList, elem, 4);
                    startingList.Remove(elem);
                    break;
                    case 3:
                    case 4:
                    addElement(startingList, elem, 1);
                    addElement(startingList, elem, 2);
                    addElement(startingList, elem, elem.Last());
                    startingList.Remove(elem);      
                    break;
                    default:
                    break;
                }
            }
        }

        static List<Int64> puzzleInputToList (string inputFilePath) 
        {
            var str = File.ReadLines (inputFilePath).First ();
            return str.Split (',').Select (Int64.Parse).ToList ();
        }

        static int fewestMovesToOxygen(List<long> puzzleInput, List<List<int>> startingList)
        {
            long xPosn = 0;
            int count = 0;
            while(xPosn!=2)
            {
                buildUpFunction(startingList);
                List<List<int>> copyOfInput = new List<List<int>>(startingList);
                foreach (var phaseSequence in copyOfInput)
                {       
                    count = 0;
                    Intcode i = new Intcode(puzzleInput, phaseSequence);
                    while (!i.hasFinished)
                    {
                        xPosn = i.Run();
                        count++;
                        if (xPosn!=1)
                        {
                            if (!i.hasFinished)  // This is required if xPosn is 0
                                startingList.Remove(phaseSequence);
                            break;
                        }
                    }
                }
            }
            return count;
        }
    }
}