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
            var startingList = new List<List<int>>(){new List<int>(){1}, 
                                                              new List<int>(){2}, 
                                                              new List<int>(){3}, 
                                                              new List<int>(){4}};

            int MAX = 1000;
            for (int j=2; j<MAX; j++)
            {
                buildUpFunction(startingList);
                List<List<int>> copyOfInput = new List<List<int>>(startingList);

                foreach (var phaseSequence in copyOfInput)
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
                            return;
                        }
                        else if (xPosn==0)
                        {
                            if (!i.hasFinished)
                                startingList.Remove(phaseSequence);
                            break;
                        }
                    }
                }
            }

            
        }

        static void buildUpFunction(List<List<int>> startingList)
        {
            List<List<int>> copyOfInput = new List<List<int>>(startingList);
            //Initially would be 1, 2, 3, 4
            foreach (var elem in copyOfInput)
            {
                switch(elem.Last())
                {
                    case 1:
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){1}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){3}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){4}).ToList());
                    startingList.Remove(elem);
                    break;
                    case 2:
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){2}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){3}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){4}).ToList());
                    startingList.Remove(elem);
                    break;
                    case 3:
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){1}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){2}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){3}).ToList());
                    startingList.Remove(elem);
                    break;
                    case 4:
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){1}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){2}).ToList());
                    startingList.Add(new List<int>(elem).Concat(new List<int>(){4}).ToList());
                    startingList.Remove(elem);
                    break;
                    default:
                    break;
                }
            }

        }

        static List<Int64> puzzleInputToList (string inputFilePath) {
            var str = File.ReadLines (inputFilePath).First ();
            var listOfInts = str.Split (',').Select (Int64.Parse).ToList ();
            return listOfInts;
        }
    }
}