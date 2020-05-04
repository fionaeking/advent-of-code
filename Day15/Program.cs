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

        static void addElement(List<List<int>> listOfMoves, List<int> elem, int numToAdd)
        {
            listOfMoves.Add(new List<int>(elem).Concat(new List<int>(){numToAdd}).ToList());
        }

        static void buildUpFunction(List<List<int>> listOfMoves)
        {
            // Need to use copy of input
            foreach (var elem in new List<List<int>>(listOfMoves))
            {
                switch(elem.Last())
                {
                    case 1:
                    case 2:
                    addElement(listOfMoves, elem, elem.Last());
                    addElement(listOfMoves, elem, 3);
                    addElement(listOfMoves, elem, 4);
                    break;
                    case 3:
                    case 4:
                    addElement(listOfMoves, elem, 1);
                    addElement(listOfMoves, elem, 2);
                    addElement(listOfMoves, elem, elem.Last());   
                    break;
                    default:
                    break;
                }
                listOfMoves.Remove(elem);
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
                foreach (var inputValue in new List<List<int>>(startingList))
                {       
                    count = 0;
                    Intcode i = new Intcode(puzzleInput, inputValue);
                    while (!i.hasFinished)
                    {
                        xPosn = i.Run();
                        count++;
                        if (xPosn!=1)
                        {
                            if (!i.hasFinished)  // This is required if xPosn is 0
                                startingList.Remove(inputValue);
                            break;
                        }
                    }
                }
            }
            return count;
        }
    }
}